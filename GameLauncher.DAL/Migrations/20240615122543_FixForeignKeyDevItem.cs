using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameLauncher.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyDevItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("4d230b99-fb69-4e93-9e7a-9215c3bc45b1"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("bce4011a-b705-4d18-993d-c8d81fec3c66"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("dbe21f56-6e46-46b6-8c5b-8e565f68b10e"));

            migrationBuilder.DropColumn(
                name: "DevID",
                table: "DevdItems");

            migrationBuilder.InsertData(
                table: "Platformes",
                columns: new[] { "ID", "CodeName", "Databases", "IgdbId", "LUProfileID", "Name" },
                values: new object[,]
                {
                    { new Guid("33b0b08a-31e6-4daf-b28a-b5e68e8ac247"), "Steam", "", null, null, "Steam" },
                    { new Guid("4efcb77a-5787-4b7f-87a0-75ff1a1b2944"), "Epic", "", null, null, "Epic Games Store" },
                    { new Guid("fe143a6a-e98d-412f-be37-6675d1824107"), "EA Play", "", null, null, "EA Origin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("33b0b08a-31e6-4daf-b28a-b5e68e8ac247"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("4efcb77a-5787-4b7f-87a0-75ff1a1b2944"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("fe143a6a-e98d-412f-be37-6675d1824107"));

            migrationBuilder.AddColumn<Guid>(
                name: "DevID",
                table: "DevdItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Platformes",
                columns: new[] { "ID", "CodeName", "Databases", "IgdbId", "LUProfileID", "Name" },
                values: new object[,]
                {
                    { new Guid("4d230b99-fb69-4e93-9e7a-9215c3bc45b1"), "Epic", "", null, null, "Epic Games Store" },
                    { new Guid("bce4011a-b705-4d18-993d-c8d81fec3c66"), "Steam", "", null, null, "Steam" },
                    { new Guid("dbe21f56-6e46-46b6-8c5b-8e565f68b10e"), "EA Play", "", null, null, "EA Origin" }
                });
        }
    }
}
