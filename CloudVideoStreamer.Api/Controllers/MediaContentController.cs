using CloudVideoStreamer.Api.Controllers.Base;
using CloudVideoStreamer.Repository.DTOs.Helpers;
using CloudVideoStreamer.Repository.DTOs.MediaContent;
using CloudVideoStreamer.Repository.DTOs.Movie;
using CloudVideoStreamer.Repository.DTOs.Paging;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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

    [HttpPost("GetFiltered")]
    public async Task<ActionResult<List<MovieDto>>> GetFiltered([FromBody] List<SortingDto> sorting,
                                                                [FromQuery] PagingDto paging, 
                                                                [FromQuery] MediaContentFilterDto model)
    {
      var result = await _mediaContentService.GetFiltered(sorting, paging, model);

      return Ok(result);
    }

    [HttpPost("Rate/{id}")]
    public async Task<ActionResult> Rate(int id, [FromBody] decimal rating)
    {
      try
      {
        await _mediaContentService.RateMediaContent(id, rating);
      }
      catch (ValidationException ex)
      {
        return NotFound(ex.Message);
      }

      return Ok();
    }
  }
}
