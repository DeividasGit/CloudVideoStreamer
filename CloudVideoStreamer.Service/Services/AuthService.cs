using Azure;
using CloudVideoStreamer.Repository.DTOs;
using CloudVideoStreamer.Repository.Interfaces;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Repository.Settings;
using CloudVideoStreamer.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Service.Services
{
  public class AuthService : IAuthService
  {
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IConfiguration configuration, IUnitOfWork unitOfWork, IUserService userService, 
      ILogger<AuthService> logger)
    {
      _configuration = configuration;
      _unitOfWork = unitOfWork;
      _userService = userService;
      _logger = logger;
    }

    public string GenerateAccessToken(User user, TimeSpan expiration)
    {
      var key = _configuration["InternalAuthKey"].ToString();
      var environmentKey = Environment.GetEnvironmentVariable(key) ?? key;

      var encryptedKey = Encoding.ASCII.GetBytes(environmentKey);
      var tokenHandler = new JwtSecurityTokenHandler();

      var tokenDescriptor = new SecurityTokenDescriptor()
      {
        Subject = new ClaimsIdentity(
          new Claim[]
          {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email)
          }),
        Expires = DateTime.UtcNow.Add(expiration),
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(encryptedKey),
          SecurityAlgorithms.HmacSha256Signature
        )
      };

      var generatedToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
      var token = tokenHandler.WriteToken(generatedToken);

      if (token == string.Empty)
        throw new SecurityTokenException("Access token not created");

      return token;
    }

    public string GenerateRefreshToken()
    {
      return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public async Task<User> ValidateUserRegistration(UserRegisterDto model) 
    {

      //var passwordValidator = new PasswordValidator<User>();

      var newUser = new User() {
        Name = model.Name,
        Email = model.Email,
        Password = model.Password
      };

      //var result = await passwordValidator.ValidateAsync(null, newUser, newUser.Password);
      //if (!result.Succeeded) 
      //{
      //  foreach (var error in result.Errors) {
      //    _logger.LogError(error.Description);
      //    throw new ValidationException(error.Description);
      //  }
      //}

      var existingUser = await _userService.Get(model);

      return existingUser;
    }

    public async Task<User> RegisterUser(UserRegisterDto model) 
    {
      var passwordHasher = new PasswordHasher<User>();

      var user = new User() {
        Name = model.Name,
        Email = model.Email,
        Password = passwordHasher.HashPassword(null, model.Password)
    };

      await _userService.Add(user);

      return user;
    }

    public async Task<User> ValidateUserLogin(UserLoginDto model)
    {
      var user = await _userService.Get(model);

      var passwordHasher = new PasswordHasher<User>();
      var result = passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);

      if(result != PasswordVerificationResult.Success)
      {
        _logger.LogWarning(result.ToString());
        throw new ValidationException(result.ToString());
      }

      return user;
    }

    public async Task<User> GetUser(int userid) {
      var user = await _userService.Get(userid);

      return user;
    }

    public async Task<RefreshToken> ValidateRefreshToken(string refreshToken, int userid, TimeSpan inactivePeriod) {
      var token = await _unitOfWork.Repository<RefreshToken, int>()
        .GetAllTrackable()
        .Where(x => x.Token == refreshToken && x.UserId == userid && !x.IsRevoked)
        .FirstOrDefaultAsync();
      if (token == null)
        throw new SecurityTokenException("Refresh token not found");

      if (token.ExpirationDate <= DateTime.UtcNow)
        throw new SecurityTokenException("Refresh token expired");

      if(token.LastUsed.Add(inactivePeriod) <= DateTime.UtcNow)
        throw new SecurityTokenException("Refresh token expired due to inactivity");

      await RevokeRefreshToken(token);

      return token;
    }

    public async Task AddRefreshToken(string refreshToken, User user, TimeSpan expiration)
    {
      _unitOfWork.Repository<RefreshToken, int>().Add(new RefreshToken()
      {
        Token = refreshToken,
        ExpirationDate = DateTime.UtcNow.Add(expiration),
        IsRevoked = false,
        UserId = user.Id,
        User = user,
        LastUsed = DateTime.UtcNow
      });

      await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateRefreshToken(RefreshToken refreshToken, string newRefreshToken, TimeSpan expiration)
    {
      refreshToken.Token = newRefreshToken;
      refreshToken.ExpirationDate = DateTime.UtcNow.Add(expiration);
      refreshToken.LastUsed = DateTime.UtcNow;

      _unitOfWork.Repository<RefreshToken, int>().Update(refreshToken);

      await _unitOfWork.SaveChangesAsync();
    }

    public async Task RevokeRefreshToken(RefreshToken refreshToken) 
    {
      refreshToken.IsRevoked = true;
      refreshToken.LastUsed = DateTime.UtcNow;

      _unitOfWork.Repository<RefreshToken, int>().Update(refreshToken);

      await _unitOfWork.SaveChangesAsync();
    }

  }
}