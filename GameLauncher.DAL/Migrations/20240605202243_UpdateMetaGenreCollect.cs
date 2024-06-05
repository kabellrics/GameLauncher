using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameLauncher.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMetaGenreCollect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("88cd4887-8092-4c12-bdb6-29edfbce7cb4"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("baf16d30-0eb6-447f-b0a2-a91e3fe68721"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("e2e0968e-0987-4b58-8115-a9daf99551a5"));

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemMetadataGenre");

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
                    { new Guid("88cd4887-8092-4c12-bdb6-29edfbce7cb4"), "Epic", "", null, null, "Epic Games Store" },
                    { new Guid("baf16d30-0eb6-447f-b0a2-a91e3fe68721"), "EA Play", "", null, null, "EA Origin" },
                    { new Guid("e2e0968e-0987-4b58-8115-a9daf99551a5"), "Steam", "", null, null, "Steam" }
                });
        }
    }
}
