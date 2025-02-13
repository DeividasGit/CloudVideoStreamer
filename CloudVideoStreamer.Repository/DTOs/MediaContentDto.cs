using CloudVideoStreamer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Repository.DTOs;

public class MediaContentDto
{
  public string Title { get; set; }
  public string Description { get; set; }
  public DateTime ReleaseDate { get; set; }
  public decimal Rating { get; set; }
}