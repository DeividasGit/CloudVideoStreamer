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
    string GenerateAccessToken(User user, TimeSpan expiration);
    string GenerateRefreshToken();
    Task<User> ValidateUserLogin(UserLoginDto model);
    Task<User> ValidateUserLogin(int userid);
    Task<RefreshToken> ValidateRefreshToken(string refreshToken, int userid, TimeSpan inactivePeriod);
    Task AddRefreshToken(string refreshToken, User user, TimeSpan expiration);
    Task UpdateRefreshToken(RefreshToken refreshToken, string newRefreshToken, TimeSpan expiration);
    Task RevokeRefreshToken(RefreshToken refreshToken);
  }
}