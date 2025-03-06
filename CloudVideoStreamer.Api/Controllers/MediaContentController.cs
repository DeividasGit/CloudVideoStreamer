using CloudVideoStreamer.Api.Controllers.Base;
using CloudVideoStreamer.Repository.DTOs.MediaContent;
using CloudVideoStreamer.Repository.DTOs.Movie;
using CloudVideoStreamer.Repository.DTOs.Paging;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudVideoStreamer.Api.Controllers {
  [ApiController]
  [Route("api/[controller]")]
  //[Authorize]
  public class MediaContentController : BaseController<MediaContent, int> 
  {
    private readonly IMediaContentService _mediaContentService;

    public MediaContentController(IMediaContentService mediaContentService) : base(mediaContentService)
    {
      _mediaContentService = mediaContentService;
    }

    [HttpGet("GetFiltered")]
    public async Task<ActionResult<List<MovieDto>>> GetFiltered([FromQuery] string sorting,
                                                                [FromQuery] PagingDto paging, 
                                                                [FromQuery] MediaContentFilterDto model)
    {
      var result = await _mediaContentService.GetFiltered(sorting, paging, model);

      return Ok(result);
    }
  }
}
