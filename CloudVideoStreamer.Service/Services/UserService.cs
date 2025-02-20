using CloudVideoStreamer.Repository.DTOs;
using CloudVideoStreamer.Repository.Interfaces;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces;
using CloudVideoStreamer.Service.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CloudVideoStreamer.Service.Services
{
  public class UserService : BaseService<User, int>, IUserService
  {
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public async Task<User> Get(UserRegisterDto model) {
      var user = await _unitOfWork.Repository<User, int>()
        .GetAll()
        .Where(x => x.Email == model.Email)
        .FirstOrDefaultAsync();

      return user;
    }

    public async Task<User> Get(UserLoginDto model)
    {
      var user = await _unitOfWork.Repository<User, int>()
        .GetAll()
        .Where(x => x.Email == model.Email)
        .FirstOrDefaultAsync();

      return user;
    }
  }
}