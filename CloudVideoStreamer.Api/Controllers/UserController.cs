using CloudVideoStreamer.Api.Controllers.Base;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces;
using CloudVideoStreamer.Service.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;

namespace CloudVideoStreamer.Api.Controllers {
  public class UserController : BaseController<User, int> {
    
    IUserService _userService;

    public UserController(IUserService userService) : base(userService) 
    {
      _userService = userService;
    }
  }
}
