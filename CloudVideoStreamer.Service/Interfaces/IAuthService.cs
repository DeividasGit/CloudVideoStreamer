using CloudVideoStreamer.Repository.DTOs;
using CloudVideoStreamer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Service.Interfaces
{
  public interface IAuthService
  {
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    Task<User> AuthenticateUser(UserLoginDto model);
    Task AddRefreshTokenToDatabase(string refreshToken, User user, TimeSpan expiration);
    Task UpdateRefreshTokenToDatabase(string refreshToken, User user, TimeSpan expiration);
    Task<RefreshToken> ValidateRefreshToken(string refreshToken, int id);
  }
}