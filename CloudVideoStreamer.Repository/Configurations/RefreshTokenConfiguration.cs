using CloudVideoStreamer.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Repository.Configurations {
  public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken> 
  {
    public void Configure(EntityTypeBuilder<RefreshToken> builder) 
    {
      builder.HasKey(x => x.Id);

      builder.Property(x => x.Token).IsRequired(true).HasMaxLength(500);
      builder.Property(x => x.IsRevoked).HasDefaultValue(false);
      builder.Property(x => x.LastUsed).IsRequired(true).HasDefaultValueSql("CURRENT_TIMESTAMP");

      builder
        .HasOne(x => x.User)
        .WithMany(x => x.RefreshTokens)
        .HasForeignKey(x => x.UserId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
