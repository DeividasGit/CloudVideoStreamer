using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudVideoStreamer.Repository.Interfaces;
using CloudVideoStreamer.Repository.Models.Base;

namespace CloudVideoStreamer.Repository.Repositories;

public class UnitOfWork : IUnitOfWork
{
  private readonly AppDbContext _context;

  public UnitOfWork(AppDbContext context)
  {
    _context = context;
  }

  public IBaseRepository<T, TK> Repository<T, TK>() where T : class, IBaseEntity<TK>
  {
    return new BaseRepository<T, TK>(_context);
  }

  public async Task<int> SaveChangesAsync()
  {
    return await _context.SaveChangesAsync();
  }
}