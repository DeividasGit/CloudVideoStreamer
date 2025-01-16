using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CloudVideoStreamer.Repository;

public class AppDbContext : DbContext
{
  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
  {
    return base.SaveChangesAsync(cancellationToken);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
  }
}