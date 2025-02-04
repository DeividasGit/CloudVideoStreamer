using CloudVideoStreamer.Repository.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Repository.Interfaces;

public interface IUnitOfWork
{
  IBaseRepository<T, TK> Repository<T, TK>() where T : class, IBaseEntity<TK>;
  Task<int> SaveChangesAsync();
}