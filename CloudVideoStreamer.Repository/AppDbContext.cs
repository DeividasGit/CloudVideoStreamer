﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudVideoStreamer.Repository.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CloudVideoStreamer.Repository;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }

  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
  {
    return base.SaveChangesAsync(cancellationToken);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new UserConfiguration());
    modelBuilder.ApplyConfiguration(new MediaContentConfiguration());
    modelBuilder.ApplyConfiguration(new MovieConfiguration());
    modelBuilder.ApplyConfiguration(new TvSeriesConfiguration());
    modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
    modelBuilder.ApplyConfiguration(new RoleConfiguration());
    modelBuilder.ApplyConfiguration(new GenreConfiguration());
    modelBuilder.ApplyConfiguration(new MediaContentGenreConfiguration());

    base.OnModelCreating(modelBuilder);
  }
}