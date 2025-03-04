using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudVideoStreamer.Repository.Migrations
{
    /// <inheritdoc />
    public partial class MediaContentGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaContentGenre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaContentId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaContentGenre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaContentGenre_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MediaContentGenre_MediaContent_MediaContentId",
                        column: x => x.MediaContentId,
                        principalTable: "MediaContent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaContentGenre_GenreId",
                table: "MediaContentGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaContentGenre_MediaContentId",
                table: "MediaContentGenre",
                column: "MediaContentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaContentGenre");

            migrationBuilder.DropTable(
                name: "Genre");
        }
    }
}
