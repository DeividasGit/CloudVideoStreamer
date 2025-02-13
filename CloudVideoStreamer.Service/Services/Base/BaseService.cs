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
}