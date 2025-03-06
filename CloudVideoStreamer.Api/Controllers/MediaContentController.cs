using CloudVideoStreamer.Api.Controllers.Base;
using CloudVideoStreamer.Repository.DTOs;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudVideoStreamer.Api.Controllers {
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class MediaContentController : BaseController<MediaContent, int> 
  {
    private readonly IMediaContentService _mediaContentService;

    public MediaContentController(IMediaContentService mediaContentService) : base(mediaContentService)
    {
      _mediaContentService = mediaContentService;
    }

    [HttpGet("GetFiltered")]
    public async Task<ActionResult<List<MovieDto>>> GetFiltered([FromQuery] MediaContentFilterDto model)
    {
      var result = await _mediaContentService.GetFiltered(model);

      return Ok(result);
    }
  }
}
