using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Service.Interfaces.Base;

public interface IBaseService<T, TK>
{
  Task<List<T>> GetAll();
  Task<T> Get(TK id);
  Task Add(T model);
  Task Update(T model);
  Task Delete(TK id);
}