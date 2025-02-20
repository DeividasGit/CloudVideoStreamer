using CloudVideoStreamer.Repository.DTOs;
using CloudVideoStreamer.Repository.Migrations;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Repository.Settings;
using CloudVideoStreamer.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace CloudVideoStreamer.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : Controller
  {
    private readonly IAuthService _authService;
    private readonly IOptions<JwtSettings> _jwtSettings;

    public AuthController(IAuthService authService, IOptions<JwtSettings> jwtSettings)
    {
      _authService = authService;
      _jwtSettings = jwtSettings;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserLoginResponseDto>> Login(UserLoginDto model)
    {
      var user = await _authService.ValidateUserLogin(model);
      if (user == null)
        return Unauthorized("Invalid credentials");

      var token = _authService.GenerateAccessToken(user, _jwtSettings.Value.AccessTokenExpiration);
      if (token == string.Empty)
        return BadRequest();

      var refreshToken = _authService.GenerateRefreshToken();
      if (refreshToken == string.Empty)
        return BadRequest();

      await _authService.AddRefreshToken(refreshToken, user, _jwtSettings.Value.RefreshTokenExpiration);

      Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions()
      {
        Secure = true,
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        Expires = DateTimeOffset.UtcNow.Add(_jwtSettings.Value.RefreshTokenExpiration)
      });

      var response = new UserLoginResponseDto()
      {
        Id = user.Id,
        Name = user.Name,
        Token = token
      };

      return Ok(response);
    }

    [HttpPost("Refresh/{id}")]
    public async Task<ActionResult<UserLoginResponseDto>> RefreshToken(int id)
    {
      var user = await _authService.ValidateUserLogin(id);
      if (user == null)
        return Unauthorized("Invalid credentials");

      var refreshToken = Request.Cookies["refresh_token"];
      if (refreshToken == string.Empty)
        Unauthorized("Refresh token not found");

      var refreshTokenObj = await _authService.ValidateRefreshToken(refreshToken, id, _jwtSettings.Value.RefreshTokenInactivity);
      if (refreshTokenObj == null)
        NotFound("Refresh token not found");

      var newtoken = _authService.GenerateAccessToken(user, _jwtSettings.Value.AccessTokenExpiration);
      if (newtoken == string.Empty)
        return BadRequest();

      var newRefreshToken = _authService.GenerateRefreshToken();
      if (newRefreshToken == string.Empty)
        BadRequest("Could not generate new token");

      await _authService.UpdateRefreshToken(refreshTokenObj, newRefreshToken, _jwtSettings.Value.RefreshTokenExpiration);

      Response.Cookies.Append("refresh_token", newRefreshToken, new CookieOptions()
      {
        Secure = true,
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        Expires = DateTimeOffset.UtcNow.Add(_jwtSettings.Value.RefreshTokenExpiration)
      });

      var response = new UserLoginResponseDto() {
        Id = user.Id,
        Name = user.Name,
        Token = newtoken
      };

      return Ok(response);
    }

    [HttpPost("Logout/{id}")]
    public async Task<ActionResult> Logout(int id) 
    {
      var refreshToken = Request.Cookies["refresh_token"];
      if (refreshToken == string.Empty)
        Unauthorized("Refresh token not found");

      var refreshTokenObj = await _authService.ValidateRefreshToken(refreshToken, id, _jwtSettings.Value.RefreshTokenInactivity);
      if (refreshTokenObj == null)
        Unauthorized("Refresh token not found");

      await _authService.RevokeRefreshToken(refreshTokenObj);

      return Ok();
    }
  }
}