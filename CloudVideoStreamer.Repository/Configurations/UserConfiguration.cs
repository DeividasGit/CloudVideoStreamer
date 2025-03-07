﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudVideoStreamer.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudVideoStreamer.Repository.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(x => x.Id);

    builder.Property(x => x.Id).UseIdentityColumn();
    builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
    builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
    builder.Property(x => x.Password).IsRequired().HasMaxLength(500);

    builder
      .HasOne(x => x.Role)
      .WithMany(x => x.Users)
      .HasForeignKey(x => x.RoleId)
      .IsRequired(true)
      .OnDelete(DeleteBehavior.Restrict);
  }
}