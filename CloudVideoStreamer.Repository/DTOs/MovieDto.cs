using CloudVideoStreamer.Repository.Models;

namespace CloudVideoStreamer.Repository.DTOs;

public class MovieDto
{
  public decimal DurationInSeconds { get; set; }
  public required MediaContentDto MediaContent { get; set; }
}