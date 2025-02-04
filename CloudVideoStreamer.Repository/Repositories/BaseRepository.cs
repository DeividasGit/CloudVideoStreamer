using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudVideoStreamer.Repository.Interfaces;
using CloudVideoStreamer.Repository.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace CloudVideoStreamer.Repository.Repositories;

public class BaseRepository<T, TK> : IBaseRepository<T, TK> where T : class, IBaseEntity<TK>
{
  private readonly AppDbContext _context;

  public BaseRepository(AppDbContext context)
  {
    _context = context;
  }

  public IQueryable<T> GetAll()
  {
    return _context.Set<T>().AsNoTracking().OrderByDescending(x => x.Id);
  }
}