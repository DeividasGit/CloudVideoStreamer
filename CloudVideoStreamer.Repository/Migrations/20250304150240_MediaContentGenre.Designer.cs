﻿// <auto-generated />
using System;
using CloudVideoStreamer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CloudVideoStreamer.Repository.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250304150240_MediaContentGenre")]
    partial class MediaContentGenre
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CloudVideoStreamer.Repository.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("CloudVideoStreamer.Repository.Models.MediaContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<decimal>("Rating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("MediaContent");
                });

            modelBuilder.Entity("CloudVideoStreamer.Repository.Models.MediaContentGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<int>("MediaContentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("MediaContentId");

                    b.ToTable("MediaContentGenre");
                });

            modelBuilder.Entity("CloudVideoStreamer.Repository.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("DurationInSeconds")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MediaContentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MediaContentId");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("CloudVideoStreamer.Repository.Models.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRevoked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("LastUsed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("CloudVideoStreamer.Repository.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValue("User");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("CloudVideoStreamer.Repository.Models.TvSeries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MediaContentId")
                        .HasColumnType("int");

                    b.Property<int>("SeasonCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MediaContentId");

                    b.ToTable("TvSeries");
                });

            modelBuilder.Entity("CloudVideoStreamer.Repository.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CloudVideoStreamer.Repository.Models.MediaContentGenre", b =>
                {
                    b.HasOne("CloudVideoStreamer.Repository.Models.Genre", "Genre")
                        .WithMany("MediaContents")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CloudVideoStreamer.Repository.Models.MediaContent", "MediaContent")
                        .WithMany("Genres")
                        .HasForeignKey("MediaContentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("MediaContent");
                });

            modelBuilder.Entity("CloudVideoStreamer.Repository.Models.Movie", b =>
                {
                    b.HasOne("CloudVideoStreamer.Repository.Models.MediaContent", "MediaContent")
                        .WithMany("Movies")
                        .HasForeignKey("MediaContentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MediaContent");
                });

            modelBuilder.Entity("CloudVideoStreamer.Repository.Models.RefreshToken", b =>
                {
                    b.HasOne("CloudVideoStreamer.Repository.Models.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CloudVideoStreamer.Repository.Models.TvSeries", b =>
                {
                    b.HasOne("CloudVideoStreamer.Repository.Models.MediaContent", "MediaContent")
                        .WithMany("TvSeries")
                        .HasForeignKey("MediaContentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MediaContent");
                });

            modelBuilder.Entity("CloudVideoStreamer.Repository.Models.User", b =>
                {
                    b.HasOne("CloudVideoStreamer.Repository.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CloudVideoStreamer.Repository.Models.Genre", b =>
                {
                    b.Navigation("MediaContents");
                });

            modelBuilder.Entity("CloudVideoStreamer.Repository.Models.MediaContent", b =>
                {
                    b.Navigation("Genres");

                    b.Navigation("Movies");

                    b.Navigation("TvSeries");
                });

            modelBuilder.Entity("CloudVideoStreamer.Repository.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("CloudVideoStreamer.Repository.Models.User", b =>
                {
                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
