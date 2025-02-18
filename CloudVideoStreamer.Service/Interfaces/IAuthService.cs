using CloudVideoStreamer.Repository.DTOs;
using CloudVideoStreamer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Service.Interfaces {
  public interface IAuthService 
  {
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    Task<User> GetUser(UserLoginDto model);
    Task StoreRefreshToken(string refreshToken, User user, TimeSpan expiration);
    Task ValidateRefreshToken(string refreshToken, int id);
  }
}
