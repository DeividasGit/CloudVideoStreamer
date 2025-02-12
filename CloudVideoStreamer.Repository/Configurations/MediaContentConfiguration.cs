using CloudVideoStreamer.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudVideoStreamer.Repository.Configurations;

public class MediaContentConfiguration : IEntityTypeConfiguration<MediaContent>
{
  public void Configure(EntityTypeBuilder<MediaContent> builder)
  {
    builder.HasKey(x => x.Id);

    builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
    builder.Property(x => x.Description).IsRequired().HasMaxLength(1000);
    builder.Property(x => x.ReleaseDate).IsRequired();
    builder.Property(x => x.Rating);
  }
}