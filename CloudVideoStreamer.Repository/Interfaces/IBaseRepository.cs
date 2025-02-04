using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Repository.Interfaces;

public interface IBaseRepository<T, TK>
{
  IQueryable<T> GetAll();
}