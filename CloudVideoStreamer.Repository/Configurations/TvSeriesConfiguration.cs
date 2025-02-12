using CloudVideoStreamer.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudVideoStreamer.Repository.Configurations;

public class TvSeriesConfiguration : IEntityTypeConfiguration<TvSeries>
{
  public void Configure(EntityTypeBuilder<TvSeries> builder)
  {
    builder.HasKey(x => x.Id);

    builder.Property(x => x.SeasonCount);

    builder
      .HasOne(x => x.MediaContent)
      .WithMany(x => x.TvSeries)
      .HasForeignKey(x => x.MediaContentId)
      .IsRequired()
      .OnDelete(DeleteBehavior.Restrict);
  }
}