using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Repository.DTOs {
  public class UserLoginResponseDto 
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Token { get; set; }
  }
}
