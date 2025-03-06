using CloudVideoStreamer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Repository.DTOs
{
  public class MediaContentFilterDto
  {
    public bool FilterMovies { get; set; } = true;
    public bool FilterTvSeries { get; set; } = true;
    public string? Title { get; set; }
    public DateTime? ReleaseDateFrom { get; set; }
    public DateTime? ReleaseDateTo { get; set; }
    public decimal? RatingFrom { get; set; }
    public decimal? RatingTo { get; set; }
    public decimal? DurationInSecondsFrom { get; set; }
    public decimal? DurationInSecondsTo { get; set; }
    public int? SeasonCountFrom { get; set; }
    public int? SeasonCountTo { get; set; }
    public List<string>? Genres { get; set; }

  }
}
