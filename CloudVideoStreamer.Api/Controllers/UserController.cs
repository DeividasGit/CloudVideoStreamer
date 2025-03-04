using CloudVideoStreamer.Api.Controllers.Base;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces;
using CloudVideoStreamer.Service.Interfaces.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudVideoStreamer.Api.Controllers {
  [ApiController]
  [Route("api/[controller]")]
  [Authorize(Roles = "Admin")]
  public class UserController : BaseController<User, int> {
    
    private readonly IUserService _userService;

    public UserController(IUserService userService) : base(userService) 
    {
      _userService = userService;
    }
  }
}
