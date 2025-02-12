using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudVideoStreamer.Repository.Migrations
{
    /// <inheritdoc />
    public partial class mainentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaContent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DurationInSeconds = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MediaContentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movie_MediaContent_MediaContentId",
                        column: x => x.MediaContentId,
                        principalTable: "MediaContent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TvSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeasonCount = table.Column<int>(type: "int", nullable: false),
                    MediaContentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TvSeries_MediaContent_MediaContentId",
                        column: x => x.MediaContentId,
                        principalTable: "MediaContent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_MediaContentId",
                table: "Movie",
                column: "MediaContentId");

            migrationBuilder.CreateIndex(
                name: "IX_TvSeries_MediaContentId",
                table: "TvSeries",
                column: "MediaContentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "TvSeries");

            migrationBuilder.DropTable(
                name: "MediaContent");
        }
    }
}
