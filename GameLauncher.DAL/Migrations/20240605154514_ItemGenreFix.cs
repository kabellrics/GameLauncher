using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameLauncher.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ItemGenreFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Items_ItemID",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_ItemID",
                table: "Genres");

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("08eb7e27-b30d-4399-823b-f6d201656851"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("1c94d50c-7f34-4e5b-8454-797bb570cf42"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("fbbb0c81-7dd7-4a6a-8804-7894a39e7f3d"));

            migrationBuilder.DropColumn(
                name: "ItemID",
                table: "Genres");

            migrationBuilder.CreateTable(
                name: "GenreItem",
                columns: table => new
                {
                    GenresID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemsID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreItem", x => new { x.GenresID, x.ItemsID });
                    table.ForeignKey(
                        name: "FK_GenreItem_Genres_GenresID",
                        column: x => x.GenresID,
                        principalTable: "Genres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreItem_Items_ItemsID",
                        column: x => x.ItemsID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Platformes",
                columns: new[] { "ID", "CodeName", "Databases", "IgdbId", "LUProfileID", "Name" },
                values: new object[,]
                {
                    { new Guid("5c5c941d-1974-4128-bbb2-7b9ed74392cc"), "Epic", "", null, null, "Epic Games Store" },
                    { new Guid("7e4267b7-0831-4020-9e62-96bfc45bfa40"), "EA Play", "", null, null, "EA Origin" },
                    { new Guid("a340b5d3-d942-4ab5-aceb-0a4833ee5f32"), "Steam", "", null, null, "Steam" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreItem_ItemsID",
                table: "GenreItem",
                column: "ItemsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreItem");

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("5c5c941d-1974-4128-bbb2-7b9ed74392cc"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("7e4267b7-0831-4020-9e62-96bfc45bfa40"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("a340b5d3-d942-4ab5-aceb-0a4833ee5f32"));

            migrationBuilder.AddColumn<Guid>(
                name: "ItemID",
                table: "Genres",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Platformes",
                columns: new[] { "ID", "CodeName", "Databases", "IgdbId", "LUProfileID", "Name" },
                values: new object[,]
                {
                    { new Guid("08eb7e27-b30d-4399-823b-f6d201656851"), "Epic", "", null, null, "Epic Games Store" },
                    { new Guid("1c94d50c-7f34-4e5b-8454-797bb570cf42"), "Steam", "", null, null, "Steam" },
                    { new Guid("fbbb0c81-7dd7-4a6a-8804-7894a39e7f3d"), "EA Play", "", null, null, "EA Origin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_ItemID",
                table: "Genres",
                column: "ItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Items_ItemID",
                table: "Genres",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ID");
        }
    }
}
