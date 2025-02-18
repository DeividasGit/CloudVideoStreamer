using CloudVideoStreamer.Repository.DTOs;
using CloudVideoStreamer.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CloudVideoStreamer.Api.Controllers {
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : Controller 
  {
    IAuthService _authService;

    public AuthController(IAuthService authService) 
    {
      _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserLoginResponseDto>> Login(UserLoginDto model) 
    {
      var user = await _authService.GetUser(model);

      if (user == null)
        return Unauthorized(ModelState);

      var token = _authService.GenerateAccessToken(user);

      if (token == string.Empty)
        return BadRequest();

      var response = new UserLoginResponseDto() 
      {
        Name = user.Name,
        Token = token
      };

      return Ok(response);
    }
  }
}
