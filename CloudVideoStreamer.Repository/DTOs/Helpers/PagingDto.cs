using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Repository.DTOs.Paging
{
  public class PagingDto
  {
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
  }
}
