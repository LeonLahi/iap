using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iap.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedAtColumnForPlaylist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Playlists",
                type: "datetimeoffset",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Playlists");
        }
    }
}
