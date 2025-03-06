using CloudVideoStreamer.Repository.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Repository.Models
{
  public class MediaContentGenre : IBaseEntity<int>
  {
    public int Id { get; set; }
    public int MediaContentId { get; set; }
    public MediaContent MediaContent { get; set; }
    public int GenreId { get; set; }
    public Genre Genre { get; set; }
  }
}
