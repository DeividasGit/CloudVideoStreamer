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

  [HttpGet]
  public virtual async Task<ActionResult<List<T>>> Get(TK id)
  {
    var result = _service.Get(id);

    if (result == null) return NotFound();

    return Ok(result);
  }

  [HttpPut]
  public virtual async Task<ActionResult> Put(T model)
  {
    await _service.Update(model);

    return Ok();
  }

  [HttpDelete]
  public virtual async Task<ActionResult> Delete(TK id)
  {
    await _service.Delete(id);

    return Ok();
  }
}