using CloudVideoStreamer.Repository.DTOs;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Service.Interfaces
{
  public interface IUserService : IBaseService<User, int>
  {
    Task<User> Get(UserLoginDto model);
  }
}