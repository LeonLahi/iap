using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iap.API.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedAsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Tracks",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tracks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Playlists",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Playlists",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Playlists");
        }
    }
}
