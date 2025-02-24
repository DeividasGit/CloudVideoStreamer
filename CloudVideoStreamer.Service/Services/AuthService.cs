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

      return token;
    }

    public string GenerateRefreshToken()
    {
      return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public async Task<UserAuthResponseDto> RegisterUser(UserRegisterDto model, 
                                                        TimeSpan accessTokenExpiration,
                                                        TimeSpan refreshTokenExpiration) 
    {
      await ValidateUserRegistration(model);

      var passwordHasher = new PasswordHasher<User>();

      var newUser = new User() {
        Name = model.Name,
        Email = model.Email,
        Password = passwordHasher.HashPassword(null, model.Password)
      };

      await _userService.Add(newUser);

      var token = GenerateAccessToken(newUser, accessTokenExpiration);
      if (string.IsNullOrEmpty(token)) {
        _logger.LogError("Could not generate access token for user: {Email}", model.Email);
        throw new SecurityTokenException("Could not generate access token");
      }

      var refreshToken = GenerateRefreshToken();
      if (refreshToken == string.Empty) {
        _logger.LogError("Could not generate refresh token for user: {Email}", model.Email);
        throw new SecurityTokenException("Could not generate refresh token");
      }

      await AddRefreshToken(refreshToken, newUser, refreshTokenExpiration);

      return new UserAuthResponseDto() {
        Id = newUser.Id,
        Name = newUser.Name,
        AccessToken = token,
        RefreshToken = refreshToken
      };
    }

    public async Task<UserAuthResponseDto> LoginUser(UserLoginDto model, TimeSpan accessTokenExpiration,
                                           TimeSpan refreshTokenExpiration) 
    {
      var user = await ValidateUserLogin(model);

      var token = GenerateAccessToken(user, accessTokenExpiration);
      if (token == string.Empty) 
      {
        _logger.LogError("Could not generate access token for user: {Email}", model.Email);
        throw new SecurityTokenException("Could not generate access token");
      }

      var refreshToken = GenerateRefreshToken();
      if (refreshToken == string.Empty) 
      {
        _logger.LogError("Could not generate refresh token for user: {Email}", model.Email);
        throw new SecurityTokenException("Could not generate refresh token");
      }

      await AddRefreshToken(refreshToken, user, refreshTokenExpiration);

      return new UserAuthResponseDto() {
        Id = user.Id,
        Name = user.Name,
        AccessToken = token,
        RefreshToken = refreshToken
      };
    }

    public async Task<UserAuthResponseDto> RefreshTokenUser(int userId, string refreshToken,
                                                            TimeSpan accessTokenExpiration,
                                                            TimeSpan refreshTokenExpiration,
                                                            TimeSpan refreshTokenInactivity) 
    {
      var user = await _userService.Get(userId);
      if (user == null) 
      {
        _logger.LogWarning("Invalid credentials for user ID: {Id}", userId);
        throw new ValidationException("Invalid credentials");
      }

      var refreshTokenObj = await ValidateRefreshToken(refreshToken, userId, refreshTokenInactivity);

      var newtoken = GenerateAccessToken(user, accessTokenExpiration);
      if (newtoken == string.Empty) {
        _logger.LogError("Failed to generate new access token for user ID: {UserId}", userId);
        throw new SecurityTokenException("Could not generate new access token");
      }

      var newRefreshToken = GenerateRefreshToken();
      if (newRefreshToken == string.Empty) {
        _logger.LogError("Failed to generate new refresh token for user ID: {UserId}", userId);
        throw new SecurityTokenException("Could not generate new refresh token");
      }

      await UpdateRefreshToken(refreshTokenObj, newRefreshToken, refreshTokenExpiration);

      return new UserAuthResponseDto() {
        Id = user.Id,
        Name = user.Name,
        AccessToken = newtoken,
        RefreshToken = refreshToken
      };
    }

    public async Task LogoutUser(int userId, string refreshToken, TimeSpan refreshTokenInactivity) 
    {
      var refreshTokenObj = await ValidateRefreshToken(refreshToken, userId, refreshTokenInactivity);

      await RevokeRefreshToken(refreshTokenObj);
    }

    public async Task ValidateUserRegistration(UserRegisterDto model) {
      var existingUser = await _userService.Get(model);
      if (existingUser != null) 
      {
        _logger.LogWarning("User already exists with this email: {Email}", model.Email);
        throw new ValidationException("User already exists");
      }
    }

    public async Task<User> ValidateUserLogin(UserLoginDto model)
    {
      var user = await _userService.Get(model);
      if (user == null) 
      {
        _logger.LogWarning("Invalid credentials for user: {Email}", model.Email);
        throw new ValidationException("Invalid credentials");
      }

      var passwordHasher = new PasswordHasher<User>();
      var result = passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);

      if(result != PasswordVerificationResult.Success)
      {
        _logger.LogWarning(result.ToString());
        throw new ValidationException(result.ToString());
      }

      return user;
    }

    public async Task<RefreshToken> ValidateRefreshToken(string refreshToken, int userid, TimeSpan inactivePeriod) {
      var token = await _unitOfWork.Repository<RefreshToken, int>()
        .GetAllTrackable()
        .Where(x => x.Token == refreshToken && x.UserId == userid && !x.IsRevoked)
        .FirstOrDefaultAsync();

      if (token == null) 
      {
        _logger.LogWarning("Refresh token not found");
        throw new SecurityTokenException("Session not found");
      }
      if (token.ExpirationDate <= DateTime.UtcNow) 
      {
        await RevokeRefreshToken(token);

        _logger.LogWarning("Refresh token expired");
        throw new SecurityTokenException("Session expired");
      }

      if (token.LastUsed.Add(inactivePeriod) <= DateTime.UtcNow) 
      {
        await RevokeRefreshToken(token);

        _logger.LogWarning("Refresh token expired due to inactivity");
        throw new SecurityTokenException("Session expired due to inactivity");
      }

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