using Microsoft.AspNetCore.Mvc;
using CloudVideoStreamer.Service.Interfaces.Base;
using Azure;
using Microsoft.AspNetCore.JsonPatch;

namespace CloudVideoStreamer.Api.Controllers.Base;

public class BaseController<T, TK> : Controller where T : class {
  private readonly IBaseService<T, TK> _service;

  public BaseController(IBaseService<T, TK> service) {
    _service = service;
  }

  [HttpGet]
  public virtual async Task<ActionResult<List<T>>> Get() {
    var result = await _service.GetAll();

    if (result == null) return NotFound();

    return Ok(result);
  }

  [HttpGet("{id}")]
  public virtual async Task<ActionResult<List<T>>> Get(TK id)
  {
    var result = await _service.Get(id);

    if (result == null) return NotFound();

    return Ok(result);
  }

  [HttpPost]
  public virtual async Task<ActionResult> Post(T model) {
    if (model == null)
      return BadRequest("Invalid request");

    await _service.Add(model);

    return Created();
  }

  [HttpPut]
  public virtual async Task<ActionResult> Put(T model)
  {
    if (model == null)
      return BadRequest("Invalid request");

    await _service.Update(model);

    return Ok();
  }

  [HttpPatch("{id}")]
  public virtual async Task<ActionResult> Patch(TK id, [FromBody] JsonPatchDocument<T> model) {
    if (model == null)
      return BadRequest("Invalid request");

    var originalModel = await _service.Get(id);
    if (originalModel == null)
      return NotFound();

    model.ApplyTo(originalModel);

    await _service.Update(originalModel);

    return Ok();
  }

  [HttpDelete("{id}")]
  public virtual async Task<ActionResult> Delete(TK id)
  {
    await _service.Delete(id);

    return Ok();
  }
}