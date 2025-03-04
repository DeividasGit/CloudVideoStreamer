using CloudVideoStreamer.Repository.Interfaces;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces;
using CloudVideoStreamer.Service.Interfaces.Base;
using CloudVideoStreamer.Service.Services.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Service.Services
{
  public class RoleService : BaseService<Role, int>, IRoleService
  {
    private readonly IUnitOfWork _unitOfWork;
    public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public async Task<Role> Get(string name)
    {
      var role = await _unitOfWork.Repository<Role, int>()
        .GetAllTrackable()
        .Where(x => x.Name == name)
        .FirstOrDefaultAsync();

      return role;
    }
  }
}
