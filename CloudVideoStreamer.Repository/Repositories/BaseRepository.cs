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

  public IQueryable<T> GetAllTrackable() {
    return _context.Set<T>().OrderByDescending(x => x.Id);
  }

  public IQueryable<T> Get(TK id)
  {
    return _context.Set<T>().Where(x => x.Id.Equals(id));
  }

  public void Add(T entity) {
    _context.Set<T>().Add(entity);
  }

  public void Add(IQueryable<T> entities) {
    _context.Set<T>().AddRange(entities);
  }

  public void Update(T entity)
  {
    _context.Set<T>().Update(entity);
  }

  public void Update(IQueryable<T> entities)
  {
    _context.Set<T>().UpdateRange(entities);
  }

  public void Delete(T entity)
  {
    _context.Set<T>().Remove(entity);
  }

  public void Delete(IQueryable<T> entities)
  {
    _context.Set<T>().RemoveRange(entities);
  }
}