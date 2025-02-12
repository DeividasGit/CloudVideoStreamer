using CloudVideoStreamer.Repository.Models.Base;

namespace CloudVideoStreamer.Repository.Models;

public class TvSeries : IBaseEntity<int>
{
  public int Id { get; set; }
  public int SeasonCount { get; set; }
  public int MediaContentId { get; set; }
  public required MediaContent MediaContent { get; set; }
}