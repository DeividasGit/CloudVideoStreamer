using CloudVideoStreamer.Repository.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Repository.DTOs.Helpers
{
  public class SortingDto
  {
    public string ColumnName { get; set; }
    public SortingOrder Order { get; set; } = SortingOrder.Ascending;
  }
}
