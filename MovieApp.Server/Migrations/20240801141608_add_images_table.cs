using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class add_images_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Actors");

            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "Directors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "Actors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                });

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "ActorId",
                keyValue: 1,
                column: "PictureId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "ActorId",
                keyValue: 2,
                column: "PictureId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "ActorId",
                keyValue: 3,
                column: "PictureId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "ActorId",
                keyValue: 4,
                column: "PictureId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "ActorId",
                keyValue: 5,
                column: "PictureId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "ActorId",
                keyValue: 6,
                column: "PictureId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "ActorId",
                keyValue: 7,
                column: "PictureId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "ActorId",
                keyValue: 8,
                column: "PictureId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "ActorId",
                keyValue: 9,
                column: "PictureId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "DirectorId",
                keyValue: 1,
                column: "PictureId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "DirectorId",
                keyValue: 2,
                column: "PictureId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "DirectorId",
                keyValue: 3,
                column: "PictureId",
                value: 3);

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ImagePath" },
                values: new object[,]
                {
                    { 1, "Sam_Mendes_in_2022-2.jpg" },
                    { 2, "Denis_Villeneuve_Cannes_2018.jpg" },
                    { 3, "Ari_Aster,_2018_(crop).jpg" },
                    { 4, "kevinspacey.jpeg" },
                    { 5, "menasuvari.jpeg" },
                    { 6, "anettebening.jpeg" },
                    { 7, "ryangosling.jpeg" },
                    { 8, "anadearmas.png" },
                    { 9, "harrisonford.jpeg" },
                    { 10, "tonicollette.jpeg" },
                    { 11, "millyshapiro.jpeg" },
                    { 12, "alexwolff.jpg" },
                    { 13, "american_beauty.jpg" },
                    { 14, "blade_runner.jpg" },
                    { 15, "hereditary.jpg" }
                });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 1,
                column: "PictureId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 2,
                column: "PictureId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 3,
                column: "PictureId",
                value: 15);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_PictureId",
                table: "Movies",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Directors_PictureId",
                table: "Directors",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_PictureId",
                table: "Actors",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Images_PictureId",
                table: "Actors",
                column: "PictureId",
                principalTable: "Images",
                principalColumn: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Directors_Images_PictureId",
                table: "Directors",
                column: "PictureId",
                principalTable: "Images",
                principalColumn: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Images_PictureId",
                table: "Movies",
                column: "PictureId",
                principalTable: "Images",
                principalColumn: "ImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Images_PictureId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Directors_Images_PictureId",
                table: "Directors");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Images_PictureId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Movies_PictureId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Directors_PictureId",
                table: "Directors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_PictureId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Actors");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Directors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "ActorId",
                keyValue: 1,
                column: "Picture",
                value: "kevinspacey.jpeg");

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "ActorId",
                keyValue: 2,
                column: "Picture",
                value: "menasuvari.jpeg");

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "ActorId",
                keyValue: 3,
                column: "Picture",
                value: "anettebening.jpeg");

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "ActorId",
                keyValue: 4,
                column: "Picture",
                value: "ryangosling.jpeg");

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "ActorId",
                keyValue: 5,
                column: "Picture",
                value: "anadearmas.jpeg");

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "ActorId",
                keyValue: 6,
                column: "Picture",
                value: "harissonford.jpeg");

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "ActorId",
                keyValue: 7,
                column: "Picture",
                value: "tonicollette.jpeg");

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "ActorId",
                keyValue: 8,
                column: "Picture",
                value: "millyshapiro.jpeg");

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "ActorId",
                keyValue: 9,
                column: "Picture",
                value: "alexwolff.jpeg");

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "DirectorId",
                keyValue: 1,
                column: "Picture",
                value: "Sam_Mendes_in_2022-2.jpg");

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "DirectorId",
                keyValue: 2,
                column: "Picture",
                value: "Denis_Villeneuve_Cannes_2018.jpg");

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "DirectorId",
                keyValue: 3,
                column: "Picture",
                value: "Ari_Aster,_2018_(crop).jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 1,
                column: "Picture",
                value: "american_beauty.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 2,
                column: "Picture",
                value: "blade_runner.jpg");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 3,
                column: "Picture",
                value: "hereditary.jpg");
        }
    }
}
