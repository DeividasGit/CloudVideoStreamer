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
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, IOptions<JwtSettings> jwtSettings, ILogger<AuthController> logger)
    {
      _authService = authService;
      _jwtSettings = jwtSettings;
      _logger = logger;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserLoginResponseDto>> Login(UserLoginDto model)
    {
      try 
      {
        var user = await _authService.ValidateUserLogin(model);
        if (user == null) 
        {
          _logger.LogWarning("Invalid credentials for user: {Email}", model.Email);
          return Unauthorized("Invalid credentials");
        }

        var token = _authService.GenerateAccessToken(user, _jwtSettings.Value.AccessTokenExpiration);
        if (token == string.Empty) 
        {
          _logger.LogError("Could not generate access token for user: {Email}", model.Email);
          return BadRequest("Could not generate access token");
        }

        var refreshToken = _authService.GenerateRefreshToken();
        if (refreshToken == string.Empty)
        {
          _logger.LogError("Could not generate refresh token for user: {Email}", model.Email);
          return BadRequest("Could not generate refresh token");
        }

        await _authService.AddRefreshToken(refreshToken, user, _jwtSettings.Value.RefreshTokenExpiration);

        Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions() {
          Secure = true,
          HttpOnly = true,
          SameSite = SameSiteMode.Strict,
          Expires = DateTimeOffset.UtcNow.Add(_jwtSettings.Value.RefreshTokenExpiration)
        });

        var response = new UserLoginResponseDto() {
          Id = user.Id,
          Name = user.Name,
          Token = token
        };

        return Ok(response);
      } catch (Exception ex) 
      {
        _logger.LogError(ex, "An error occurred during login for user: {Email}", model.Email);
        return StatusCode(500, "Internal server error");
      }
    }

    [HttpPost("Refresh/{id}")]
    public async Task<ActionResult<UserLoginResponseDto>> RefreshToken(int id)
    {
      try 
      {
        var user = await _authService.ValidateUserLogin(id);
        if (user == null) {
          _logger.LogWarning("Invalid credentials for user ID: {Id}", id);
          return Unauthorized("Invalid credentials");
        }

        var refreshToken = Request.Cookies["refresh_token"];
        if (refreshToken == string.Empty) {
          _logger.LogWarning("Refresh token not found for user ID: {UserId}", id);
          return Unauthorized("Refresh token not found");
        }

        var refreshTokenObj = await _authService.ValidateRefreshToken(refreshToken, id, _jwtSettings.Value.RefreshTokenInactivity);
        if (refreshTokenObj == null) {
          _logger.LogWarning("Invalid refresh token for user ID: {UserId}", id);
          return NotFound("Refresh token not found");
        }

        var newtoken = _authService.GenerateAccessToken(user, _jwtSettings.Value.AccessTokenExpiration);
        if (newtoken == string.Empty) {
          _logger.LogError("Failed to generate new access token for user ID: {UserId}", id);
          return BadRequest("Could not generate new access token");
        }

        var newRefreshToken = _authService.GenerateRefreshToken();
        if (newRefreshToken == string.Empty) {
          _logger.LogError("Failed to generate new refresh token for user ID: {UserId}", id);
          return BadRequest("Could not generate new refresh token");
        }

        await _authService.UpdateRefreshToken(refreshTokenObj, newRefreshToken, _jwtSettings.Value.RefreshTokenExpiration);

        Response.Cookies.Append("refresh_token", newRefreshToken, new CookieOptions() {
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
      } catch (Exception ex) 
      {
        _logger.LogError(ex, "An error occurred while refreshing token for user ID: {UserId}", id);
        return StatusCode(500, "Internal server error");
      }
    }

    [HttpPost("Logout/{id}")]
    public async Task<ActionResult> Logout(int id) 
    {
      try 
      {
        var refreshToken = Request.Cookies["refresh_token"];
        if (refreshToken == string.Empty) 
        {
          _logger.LogWarning("Refresh token not found for user ID: {Id}", id);
          return Unauthorized("Refresh token not found");
        }

        var refreshTokenObj = await _authService.ValidateRefreshToken(refreshToken, id, _jwtSettings.Value.RefreshTokenInactivity);
        if (refreshTokenObj == null) 
        {
          _logger.LogWarning("Refresh token not found for user ID: {Id}", id);
          return Unauthorized("Refresh token not found");
        }

        await _authService.RevokeRefreshToken(refreshTokenObj);

        return Ok();
      } catch (Exception ex) 
      {
        _logger.LogError(ex, "An error occurred while loging out user ID: {UserId}", id);
        return StatusCode(500, "Internal server error");
      }
    }
  }
}