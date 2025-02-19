using CloudVideoStreamer.Repository.DTOs;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Repository.Settings;
using CloudVideoStreamer.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CloudVideoStreamer.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : Controller
  {
    private readonly IAuthService _authService;
    private readonly JwtSettings _jwtSettings;

    public AuthController(IAuthService authService, JwtSettings jwtSettings)
    {
      _authService = authService;
      _jwtSettings = jwtSettings;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserLoginResponseDto>> Login(UserLoginDto model)
    {
      //todo get from user service(validate user)
      var user = await _authService.AuthenticateUser(model);
      if (user == null)
        return Unauthorized("Invalid credentials");

      var token = _authService.GenerateAccessToken(user);
      if (token == string.Empty)
        return BadRequest();

      var refreshToken = _authService.GenerateRefreshToken();
      if (refreshToken == string.Empty)
        return BadRequest();

      Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions()
      {
        Secure = true,
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        Expires = DateTimeOffset.UtcNow.Add(_jwtSettings.RefreshTokenExpiration)
      });

      await _authService.AddRefreshTokenToDatabase(refreshToken, user, _jwtSettings.RefreshTokenExpiration);

      var response = new UserLoginResponseDto()
      {
        Id = user.Id,
        Name = user.Name,
        Token = token
      };

      return Ok(response);
    }

    [HttpPost("Refresh/{id}")]
    public async Task<ActionResult> RefreshToken(int id)
    {
      var refreshToken = Request.Cookies["refresh_token"];
      if (refreshToken == string.Empty)
        Unauthorized("Refresh token not found");

      await _authService.ValidateRefreshToken(refreshToken, id);

      var newRefreshToken = _authService.GenerateRefreshToken();
      if (newRefreshToken == string.Empty)
        BadRequest("Could not generate new token");

      Response.Cookies.Append("refresh_token", newRefreshToken, new CookieOptions()
      {
        Secure = true,
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        Expires = DateTimeOffset.UtcNow.Add(_jwtSettings.RefreshTokenExpiration)
      });

      await _authService.UpdateRefreshTokenToDatabase(refreshToken, user, _jwtSettings.RefreshTokenExpiration);

      return Ok();
    }
  }
}