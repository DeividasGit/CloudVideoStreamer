using CloudVideoStreamer.Repository.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Repository.Models {
  public class RefreshToken : IBaseEntity<int>
  {
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsRevoked { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
  }
}
