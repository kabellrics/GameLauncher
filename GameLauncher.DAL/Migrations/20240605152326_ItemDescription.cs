using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameLauncher.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ItemDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Platformes",
                columns: new[] { "ID", "CodeName", "Databases", "IgdbId", "LUProfileID", "Name" },
                values: new object[,]
                {
                    { new Guid("08eb7e27-b30d-4399-823b-f6d201656851"), "Epic", "", null, null, "Epic Games Store" },
                    { new Guid("1c94d50c-7f34-4e5b-8454-797bb570cf42"), "Steam", "", null, null, "Steam" },
                    { new Guid("fbbb0c81-7dd7-4a6a-8804-7894a39e7f3d"), "EA Play", "", null, null, "EA Origin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Description",
                table: "Items");

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
    }
}
