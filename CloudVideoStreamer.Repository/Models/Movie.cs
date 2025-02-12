using System.ComponentModel.DataAnnotations;
using CloudVideoStreamer.Repository.Models.Base;

namespace CloudVideoStreamer.Repository.Models;

public class Movie : IBaseEntity<int>
{
  public int Id { get; set; }
  public decimal DurationInSeconds { get; set; }
  public int MediaContentId { get; set; }
  public required MediaContent MediaContent { get; set; }
}