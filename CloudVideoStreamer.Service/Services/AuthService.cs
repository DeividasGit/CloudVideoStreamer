using CloudVideoStreamer.Repository.DTOs;
using CloudVideoStreamer.Repository.Interfaces;
using CloudVideoStreamer.Repository.Models;
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

namespace CloudVideoStreamer.Service.Services {
  public class AuthService : IAuthService {

    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(IConfiguration configuration, IUnitOfWork unitOfWork) 
    {
      _configuration = configuration;
      _unitOfWork = unitOfWork;
    }
    public string GenerateAccessToken(User user) {
      var key = _configuration["InternalAuthKey"].ToString();
      var environmentKey = Environment.GetEnvironmentVariable(key) ?? key;

      var encryptedKey = Encoding.ASCII.GetBytes(environmentKey);
      var tokenHandler = new JwtSecurityTokenHandler();

      var tokenDescriptor = new SecurityTokenDescriptor() {
        Subject = new ClaimsIdentity(
          new Claim[] {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email)
          }),
        Expires = DateTime.UtcNow.AddHours(1),
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

    public string GenerateRefreshToken() {
      return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public async Task<User> GetUser(UserLoginDto model) 
    {
      var user = await _unitOfWork.Repository<User, int>()
        .GetAllTrackable()
        .Where(x => x.Email == model.Email && x.Password == model.Password)
        .FirstOrDefaultAsync();

      return user;
    }
  }
}
