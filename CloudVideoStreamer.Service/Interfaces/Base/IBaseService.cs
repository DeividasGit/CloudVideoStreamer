using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Service.Interfaces.Base;

public interface IBaseService<T, TK>
{
  IQueryable<T> GetAll();
}