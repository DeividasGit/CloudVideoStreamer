﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudVideoStreamer.Repository.Models.Base;

namespace CloudVideoStreamer.Repository.Models;

public class User : IBaseEntity<int>
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string Password { get; set; }
  public int RoleId { get; set; }
  public Role Role { get; set; }
  public List<RefreshToken> RefreshTokens { get; set; }
}