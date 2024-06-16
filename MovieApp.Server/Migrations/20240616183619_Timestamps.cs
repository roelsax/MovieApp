using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class Timestamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Aangepast",
                table: "Movies",
                type: "timestamp",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Aangepast",
                table: "Directors",
                type: "timestamp",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Aangepast",
                table: "Actors",
                type: "timestamp",
                rowVersion: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aangepast",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Aangepast",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "Aangepast",
                table: "Actors");
        }
    }
}
