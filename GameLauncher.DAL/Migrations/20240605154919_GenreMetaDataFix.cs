using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameLauncher.DAL.Migrations
{
    /// <inheritdoc />
    public partial class GenreMetaDataFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MetadataGenres_Genres_GenreId",
                table: "MetadataGenres");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "GenreId",
                table: "MetadataGenres",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.InsertData(
                table: "Platformes",
                columns: new[] { "ID", "CodeName", "Databases", "IgdbId", "LUProfileID", "Name" },
                values: new object[,]
                {
                    { new Guid("88cd4887-8092-4c12-bdb6-29edfbce7cb4"), "Epic", "", null, null, "Epic Games Store" },
                    { new Guid("baf16d30-0eb6-447f-b0a2-a91e3fe68721"), "EA Play", "", null, null, "EA Origin" },
                    { new Guid("e2e0968e-0987-4b58-8115-a9daf99551a5"), "Steam", "", null, null, "Steam" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataGenres_Genres_GenreId",
                table: "MetadataGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MetadataGenres_Genres_GenreId",
                table: "MetadataGenres");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "GenreId",
                table: "MetadataGenres",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Platformes",
                columns: new[] { "ID", "CodeName", "Databases", "IgdbId", "LUProfileID", "Name" },
                values: new object[,]
                {
                    { new Guid("5c5c941d-1974-4128-bbb2-7b9ed74392cc"), "Epic", "", null, null, "Epic Games Store" },
                    { new Guid("7e4267b7-0831-4020-9e62-96bfc45bfa40"), "EA Play", "", null, null, "EA Origin" },
                    { new Guid("a340b5d3-d942-4ab5-aceb-0a4833ee5f32"), "Steam", "", null, null, "Steam" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MetadataGenres_Genres_GenreId",
                table: "MetadataGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
