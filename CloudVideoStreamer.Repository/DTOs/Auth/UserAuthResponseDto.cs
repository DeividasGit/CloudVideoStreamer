using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Repository.DTOs.Auth
{
    public class UserAuthResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
