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

    [HttpGet("{name:alpha}")]
    public async Task<ActionResult<Role>> Get(string name)
    {
      var role = await _roleService.Get(name);
      if (role == null)
        return NotFound();

      return Ok(role);
    }
  }
}
