using CloudVideoStreamer.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Repository.Interfaces;

public interface IBaseRepository<T, TK>
{
  IQueryable<T> GetAll();
  IQueryable<T> GetAllTrackable();
  IQueryable<T> Get(TK id);
  void Add(T entity);
  void Add(IQueryable<T> entities);
  void Update(T entity);
  void Update(IQueryable<T> entities);
  void Delete(T entity);
  void Delete(IQueryable<T> entities);
}