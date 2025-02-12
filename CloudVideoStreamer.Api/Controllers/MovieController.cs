using CloudVideoStreamer.Api.Controllers.Base;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CloudVideoStreamer.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class MovieController : BaseController<Movie, int>
  {
    private IMovieService _movieService;

    public MovieController(IMovieService movieService) : base(movieService)
    {
      _movieService = movieService;
    }

    [HttpPost("Rate")]
    public async Task<ActionResult<Movie>> Rate()
    {
      var result = 1;

      return Ok(result);
    }
  }
}