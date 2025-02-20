using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudVideoStreamer.Repository.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenLastUsed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUsed",
                table: "RefreshToken",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUsed",
                table: "RefreshToken");
        }
    }
}
