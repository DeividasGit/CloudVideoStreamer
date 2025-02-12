using CloudVideoStreamer.Repository.Models.Base;

namespace CloudVideoStreamer.Repository.Models;

public class MediaContent : IBaseEntity<int>
{
  public int Id { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public DateTime ReleaseDate { get; set; }
  public decimal Rating { get; set; }
  public ICollection<Movie> Movies { get; set; }
  public ICollection<TvSeries> TvSeries { get; set; }
}