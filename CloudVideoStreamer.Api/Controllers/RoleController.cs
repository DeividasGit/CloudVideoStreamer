using CloudVideoStreamer.Api.Controllers.Base;
using CloudVideoStreamer.Repository.Interfaces;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudVideoStreamer.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize(Roles = "Admin")]
  public class RoleController : BaseController<Role, int>
  {
    private readonly IRoleService _roleService;
    public RoleController(IRoleService roleService) : base(roleService)
    {
      _roleService = roleService;
    }
  }
}
