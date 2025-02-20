﻿using CloudVideoStreamer.Repository.DTOs;
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
    Task<User> ValidateUserLogin(UserLoginDto model);
    Task<RefreshToken> ValidateRefreshToken(string refreshToken, int userid);
    Task AddRefreshTokenToDatabase(string refreshToken, User user, TimeSpan expiration);
    Task UpdateRefreshTokenToDatabase(RefreshToken refreshToken, string newRefreshToken, TimeSpan expiration);
  }
}