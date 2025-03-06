using CloudVideoStreamer.Repository.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Repository.Models
{
  public class Genre : IBaseEntity<int>
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<MediaContentGenre> MediaContents { get; set; }
  }
}
