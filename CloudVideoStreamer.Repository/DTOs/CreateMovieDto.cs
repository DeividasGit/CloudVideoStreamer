using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Repository.DTOs {
  public class CreateMovieDto {
    public decimal DurationInSeconds { get; set; }
    public int MediaContentId { get; set; }
  }
}
