using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameLauncher.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SetGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemMetadataGenre");

            migrationBuilder.DropTable(
                name: "MetadataGenres");

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("04238591-e166-4403-a53e-590debd243eb"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("1a1c6ae1-b68e-4bc8-8a65-b1b0071d4eed"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("d668c5af-83da-499e-985b-d63fb1d05a71"));

            migrationBuilder.InsertData(
                table: "Platformes",
                columns: new[] { "ID", "CodeName", "Databases", "IgdbId", "LUProfileID", "Name" },
                values: new object[,]
                {
                    { new Guid("42d3de15-0041-4a26-a4e3-06e34f029a70"), "EA Play", "", null, null, "EA Origin" },
                    { new Guid("9cfb55d2-e47f-4cb3-b2c0-65789cdfe620"), "Steam", "", null, null, "Steam" },
                    { new Guid("fc042e25-0ea5-4db2-b242-7aa4c60165de"), "Epic", "", null, null, "Epic Games Store" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("42d3de15-0041-4a26-a4e3-06e34f029a70"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("9cfb55d2-e47f-4cb3-b2c0-65789cdfe620"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("fc042e25-0ea5-4db2-b242-7aa4c60165de"));

            migrationBuilder.CreateTable(
                name: "MetadataGenres",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    GenreId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataGenres", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MetadataGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ItemMetadataGenre",
                columns: table => new
                {
                    ItemsID = table.Column<Guid>(type: "TEXT", nullable: false),
                    MetadataGenresID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMetadataGenre", x => new { x.ItemsID, x.MetadataGenresID });
                    table.ForeignKey(
                        name: "FK_ItemMetadataGenre_Items_ItemsID",
                        column: x => x.ItemsID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemMetadataGenre_MetadataGenres_MetadataGenresID",
                        column: x => x.MetadataGenresID,
                        principalTable: "MetadataGenres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Platformes",
                columns: new[] { "ID", "CodeName", "Databases", "IgdbId", "LUProfileID", "Name" },
                values: new object[,]
                {
                    { new Guid("04238591-e166-4403-a53e-590debd243eb"), "EA Play", "", null, null, "EA Origin" },
                    { new Guid("1a1c6ae1-b68e-4bc8-8a65-b1b0071d4eed"), "Steam", "", null, null, "Steam" },
                    { new Guid("d668c5af-83da-499e-985b-d63fb1d05a71"), "Epic", "", null, null, "Epic Games Store" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemMetadataGenre_MetadataGenresID",
                table: "ItemMetadataGenre",
                column: "MetadataGenresID");

            migrationBuilder.CreateIndex(
                name: "IX_MetadataGenres_GenreId",
                table: "MetadataGenres",
                column: "GenreId");
        }
    }
}
