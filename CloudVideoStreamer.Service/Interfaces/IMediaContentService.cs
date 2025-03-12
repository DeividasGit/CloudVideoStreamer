using CloudVideoStreamer.Repository.DTOs.MediaContent;
using CloudVideoStreamer.Repository.DTOs.Paging;
using CloudVideoStreamer.Repository.Models;
using CloudVideoStreamer.Service.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Service.Interfaces {
  public interface IMediaContentService : IBaseService<MediaContent, int>
  {
    Task<List<MediaContentDto>> GetFiltered(string sorting, PagingDto paging, MediaContentFilterDto model);
    Task RateMediaContent(int id, decimal rating);
  }
}
