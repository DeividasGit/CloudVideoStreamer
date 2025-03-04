using CloudVideoStreamer.Repository.Models.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Repository.Models
{
  public class Role : IBaseEntity<int>
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<User> Users { get; set;}
  }
}
