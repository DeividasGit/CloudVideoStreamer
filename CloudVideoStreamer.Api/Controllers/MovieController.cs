using CloudVideoStreamer.Api.Controllers.Base;
using CloudVideoStreamer.Repository.DTOs;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudVideoStreamer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class MovieController : BaseController<Movie, int>
{
  private IMovieService _movieService;

  public MovieController(IMovieService movieService) : base(movieService)
  {
    _movieService = movieService;
  }

  [HttpGet("GetDto")]
  public async Task<ActionResult<List<MovieDto>>> GetDto()
  {
    var result = await _movieService.GetAll();

    if (result == null)
      return NotFound();

    return Ok(result);
  }

  [HttpPost("PostDto")]
  public async Task<ActionResult> PostDto(CreateMovieDto model) {

    var result = _movieService.Add(model);

    if (result.Status == TaskStatus.Faulted)
      return BadRequest(result);

    return Created();
  }

  [HttpPut("PutDto")]
  public async Task<ActionResult> PutDto(UpdateMovieDto model) {
    var result = _movieService.Update(model);

    if (result.Status == TaskStatus.Faulted)
      return BadRequest(result);

    return Ok();
  }
}