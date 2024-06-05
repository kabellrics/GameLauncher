using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameLauncher.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FavoriteItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("3ac48107-1aed-4065-98c7-a54351e7a892"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("5ee9bded-6dcf-43db-a4ce-7f2fb23415e7"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("dfd2dc9e-45b1-47e4-b3d7-facf70ca307d"));

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Platformes",
                columns: new[] { "ID", "CodeName", "Databases", "IgdbId", "LUProfileID", "Name" },
                values: new object[,]
                {
                    { new Guid("63bc39f7-3e97-40b3-9693-8cb6a9b24ab6"), "Epic", "", null, null, "Epic Games Store" },
                    { new Guid("8b01f713-3edf-4109-b812-9aad9843e3de"), "Steam", "", null, null, "Steam" },
                    { new Guid("e4fe562b-3c61-45d1-8b15-66e43a9417e5"), "EA Play", "", null, null, "EA Origin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("63bc39f7-3e97-40b3-9693-8cb6a9b24ab6"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("8b01f713-3edf-4109-b812-9aad9843e3de"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("e4fe562b-3c61-45d1-8b15-66e43a9417e5"));

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Items");

            migrationBuilder.InsertData(
                table: "Platformes",
                columns: new[] { "ID", "CodeName", "Databases", "IgdbId", "LUProfileID", "Name" },
                values: new object[,]
                {
                    { new Guid("3ac48107-1aed-4065-98c7-a54351e7a892"), "Epic", "", null, null, "Epic Games Store" },
                    { new Guid("5ee9bded-6dcf-43db-a4ce-7f2fb23415e7"), "EA Play", "", null, null, "EA Origin" },
                    { new Guid("dfd2dc9e-45b1-47e4-b3d7-facf70ca307d"), "Steam", "", null, null, "Steam" }
                });
        }
    }
}
