﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Repository.Models.Base;

public interface IBaseEntity<T>
{
  T Id { get; set; }
}