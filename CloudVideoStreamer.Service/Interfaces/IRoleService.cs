﻿using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Service.Interfaces
{
  public interface IRoleService : IBaseService<Role, int>
  {
    Task<Role> Get(string name);
  }
}
