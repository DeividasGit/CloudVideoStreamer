using CloudVideoStreamer.Api.Controllers.Base;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CloudVideoStreamer.Api.Controllers {
  [ApiController]
  [Route("api/[controller]")]
  public class MediaContentController : BaseController<MediaContent, int> 
  {
    private readonly IMediaContentService _mediaContentService;

    public MediaContentController(IMediaContentService mediaContentService) : base(mediaContentService)
    {
      _mediaContentService = mediaContentService;
    }
  }
}
