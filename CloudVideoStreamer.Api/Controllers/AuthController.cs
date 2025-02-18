using CloudVideoStreamer.Repository.DTOs;
using CloudVideoStreamer.Repository.Settings;
using CloudVideoStreamer.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CloudVideoStreamer.Api.Controllers {
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
      var user = await _authService.GetUser(model);

      if (user == null)
        return Unauthorized("Invalid credentials");

      var token = _authService.GenerateAccessToken(user);

      if (token == string.Empty)
        return BadRequest();

      var refreshToken = _authService.GenerateRefreshToken();

      if (refreshToken == string.Empty)
        return BadRequest();

      Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions() {
        Secure = true,
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        Expires = DateTimeOffset.UtcNow.Add(_jwtSettings.RefreshTokenExpiration)
      });

      await _authService.StoreRefreshToken(refreshToken, user, _jwtSettings.RefreshTokenExpiration);

      var response = new UserLoginResponseDto() 
      {
        Name = user.Name,
        Token = token
      };

      return Ok(response);
    }
  }
}
