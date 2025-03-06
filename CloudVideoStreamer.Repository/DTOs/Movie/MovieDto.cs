using CloudVideoStreamer.Repository.DTOs.MediaContent;
using CloudVideoStreamer.Repository.Models;

namespace CloudVideoStreamer.Repository.DTOs.Movie;

public class MovieDto
{
    public int Id { get; set; }
    public decimal DurationInSeconds { get; set; }
    public required MediaContentDto MediaContent { get; set; }
}