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
  public virtual async Task<ActionResult<List<T>>> Get()
  {
    var result = await _service.GetAll();

    if (result == null) return NotFound();

    return Ok(result);
  }
}