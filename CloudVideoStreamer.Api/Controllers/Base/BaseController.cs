using Microsoft.AspNetCore.Mvc;
using CloudVideoStreamer.Service.Interfaces.Base;

namespace CloudVideoStreamer.Api.Controllers.Base;

public class BaseController<T, TK> : Controller
{
  private readonly IBaseService<T, TK> _service;

  public BaseController(IBaseService<T, TK> service)
  {
    _service = service;
  }

  [HttpGet]
  public virtual IQueryable<T> Get()
  {
    return _service.GetAll();
  }
}