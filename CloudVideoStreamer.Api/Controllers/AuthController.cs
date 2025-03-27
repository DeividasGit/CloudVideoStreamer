using CloudVideoStreamer.Repository.DTOs.Auth;
using CloudVideoStreamer.Repository.Migrations;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Repository.Settings;
using CloudVideoStreamer.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

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

    [HttpPost("Register")]
    public async Task<ActionResult<UserLoginResponseDto>> Register(UserRegisterDto model) {
      try
      {
        if (!ModelState.IsValid)
        {
          _logger.LogWarning("Email or password not valid");
          return BadRequest(ModelState);
        }

        var response = await _authService
          .RegisterUser(model, 
                        _jwtSettings.Value.AccessTokenExpiration,
                        _jwtSettings.Value.RefreshTokenExpiration);
        if (response == null) 
          return BadRequest("Failed to register");
        
        SetRefreshTokenCookie(response.RefreshToken);

        return Ok(new UserLoginResponseDto() {
          Id = response.Id,
          Name = response.Name,
          Token = response.AccessToken
        });       
      } 
      catch (ValidationException ex) 
      {
        return Unauthorized(ex.Message);
      } 
      catch (SecurityTokenException ex) 
      {
        return BadRequest(ex.Message);
      } 
      catch (Exception ex) 
      {
        _logger.LogError(ex, "Register error for user: {Email}", model.Email);
        return StatusCode(500, "Internal server error");
      }
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserLoginResponseDto>> Login(UserLoginDto model)
    {
      try 
      {
        if (!ModelState.IsValid) 
        {
          _logger.LogWarning("Email or password not valid");
          return BadRequest(ModelState);
        }

        var response = await _authService
          .LoginUser(model,
                     _jwtSettings.Value.AccessTokenExpiration,
                     _jwtSettings.Value.RefreshTokenExpiration);
        if (response == null)
          return BadRequest("Failed to log in");

        SetRefreshTokenCookie(response.RefreshToken);

        return Ok(new UserLoginResponseDto() {
          Id = response.Id,
          Name = response.Name,
          Token = response.AccessToken
        });
      } 
      catch (ValidationException ex) 
      {
        return Unauthorized(ex.Message);
      } 
      catch (SecurityTokenException ex) 
      {
        return BadRequest(ex.Message);
      } 
      catch (Exception ex) 
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
        var refreshToken = Request.Cookies["refresh_token"];
        if (refreshToken == string.Empty) {
          _logger.LogWarning("Refresh token not found for user ID: {UserId}", id);
          return Unauthorized("Refresh token not found");
        }

        var response = await _authService
             .RefreshTokenUser(id, refreshToken,
             _jwtSettings.Value.AccessTokenExpiration,
             _jwtSettings.Value.RefreshTokenExpiration,
             _jwtSettings.Value.RefreshTokenInactivity);
        if (response == null)
          return BadRequest("Failed to log in");

        SetRefreshTokenCookie(response.RefreshToken);

        return Ok(new UserLoginResponseDto() {
          Id = response.Id,
          Name = response.Name,
          Token = response.AccessToken
        });
      } 
      catch (ValidationException ex) 
      {
        return Unauthorized(ex.Message);
      } 
      catch (SecurityTokenException ex) 
      {
        return BadRequest(ex.Message);
      }
      catch (Exception ex) 
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

        await _authService.LogoutUser(id, refreshToken, 
                                      _jwtSettings.Value.RefreshTokenInactivity);

        return Ok();
      } 
      catch (ValidationException ex) 
      {
        return Unauthorized(ex.Message);
      } 
      catch (Exception ex) 
      {
        _logger.LogError(ex, "An error occurred while loging out user ID: {UserId}", id);
        return StatusCode(500, "Internal server error");
      }
    }

    private void SetRefreshTokenCookie(string refreshToken) {
      Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions() {
        Secure = true,
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        Expires = DateTimeOffset.UtcNow.Add(_jwtSettings.Value.RefreshTokenExpiration)
      });
    }

  }
}