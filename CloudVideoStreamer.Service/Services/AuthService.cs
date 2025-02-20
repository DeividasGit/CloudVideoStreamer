using Azure;
using CloudVideoStreamer.Repository.DTOs;
using CloudVideoStreamer.Repository.Interfaces;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Repository.Settings;
using CloudVideoStreamer.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
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

    public AuthService(IConfiguration configuration, IUnitOfWork unitOfWork, IUserService userService)
    {
      _configuration = configuration;
      _unitOfWork = unitOfWork;
      _userService = userService;
    }

    public string GenerateAccessToken(User user)
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
        Expires = DateTime.UtcNow.AddMinutes(30),
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

    public async Task<User> ValidateUserLogin(UserLoginDto model)
    {
      var user = await _userService.Get(model);

      return user;
    }

    public async Task<RefreshToken> ValidateRefreshToken(string refreshToken, int userid) {
      var token = await _unitOfWork.Repository<RefreshToken, int>()
        .GetAllTrackable()
        .Where(x => x.Token == refreshToken && x.UserId == userid && !x.IsRevoked)
        .FirstOrDefaultAsync();

      if (token == null)
        throw new SecurityTokenException("Refresh token not found");

      return token;
    }

    public async Task AddRefreshTokenToDatabase(string refreshToken, User user, TimeSpan expiration)
    {
      _unitOfWork.Repository<RefreshToken, int>().Add(new RefreshToken()
      {
        Token = refreshToken,
        ExpirationDate = DateTime.UtcNow.Add(expiration),
        IsRevoked = false,
        UserId = user.Id,
        User = user
      });

      await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateRefreshTokenToDatabase(RefreshToken refreshToken, string newRefreshToken, TimeSpan expiration)
    {
      refreshToken.Token = newRefreshToken;
      refreshToken.ExpirationDate = DateTime.UtcNow.Add(expiration);

      _unitOfWork.Repository<RefreshToken, int>().Update(refreshToken);

      await _unitOfWork.SaveChangesAsync();
    }

  }
}