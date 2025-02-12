using CloudVideoStreamer.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudVideoStreamer.Repository.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
  public void Configure(EntityTypeBuilder<Movie> builder)
  {
    builder.HasKey(x => x.Id);

    builder.Property(x => x.DurationInSeconds).IsRequired();

    builder
      .HasOne(x => x.MediaContent)
      .WithMany(x => x.Movies)
      .HasForeignKey(x => x.MediaContentId)
      .IsRequired()
      .OnDelete(DeleteBehavior.Restrict);
  }
}