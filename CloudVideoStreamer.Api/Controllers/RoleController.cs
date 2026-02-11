using CloudVideoStreamer.Api.Controllers.Base;
using CloudVideoStreamer.Repository.Interfaces;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Repository.Repositories;
using CloudVideoStreamer.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudVideoStreamer.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize(Roles = "Admin")]
  public class RoleController : BaseController<Role, int>
  {
    private readonly IRoleService _roleService;
    private readonly IUnitOfWork _unitOfWork;
    public RoleController(IRoleService roleService, IUnitOfWork unitOfWork) : base(roleService)
    {
        _roleService = roleService;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{name:alpha}")]
    public async Task<ActionResult<Role>> Get(string name)
    {
      var role = await _unitOfWork.Repository<Role, int>()
          .GetAllTrackable()
          .Where(x => x.Name == name)
          .FirstOrDefaultAsync();

      if (role == null)
      return NotFound();

      return Ok(role);
    }
  }
}
