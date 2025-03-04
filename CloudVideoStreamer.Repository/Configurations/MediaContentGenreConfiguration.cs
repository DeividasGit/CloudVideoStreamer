using CloudVideoStreamer.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudVideoStreamer.Repository.Configurations
{
  public class MediaContentGenreConfiguration : IEntityTypeConfiguration<MediaContentGenre>
  {
    public void Configure(EntityTypeBuilder<MediaContentGenre> builder)
    {
      builder.HasKey(x => x.Id);

      builder
        .HasOne(x => x.MediaContent)
        .WithMany(x => x.Genres)
        .HasForeignKey(x => x.MediaContentId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Restrict);

      builder
        .HasOne(x => x.Genre)
        .WithMany(x => x.MediaContents)
        .HasForeignKey(x => x.GenreId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
