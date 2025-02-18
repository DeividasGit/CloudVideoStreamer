using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudVideoStreamer.Repository.Interfaces;
using CloudVideoStreamer.Repository.Models.Base;
using CloudVideoStreamer.Service.Interfaces.Base;
using CloudVideoStreamer.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CloudVideoStreamer.Service.Services.Base;

public class BaseService<T, TK> : IBaseService<T, TK> where T : class, IBaseEntity<TK>
{
  private readonly IUnitOfWork _unitOfWork;

  public BaseService(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public virtual async Task<List<T>> GetAll()
  {
    return await _unitOfWork.Repository<T, TK>().GetAll().ToListAsync();
  }

  public virtual async Task<T> Get(TK id)
  {
    return await _unitOfWork.Repository<T, TK>().Get(id).SingleAsync();
  }

  public virtual async Task Add(T model) {
    _unitOfWork.Repository<T, TK>().Add(model);

    await _unitOfWork.SaveChangesAsync();
  }

  public virtual async Task Update(T model)
  {
    _unitOfWork.Repository<T, TK>().Update(model);

    await _unitOfWork.SaveChangesAsync();
  }

  public virtual async Task Delete(TK id)
  {
    var entity = await _unitOfWork.Repository<T, TK>().Get(id).SingleAsync();

    _unitOfWork.Repository<T, TK>().Delete(entity);

    await _unitOfWork.SaveChangesAsync();
  }
}