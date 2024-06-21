using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameLauncher.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCollectionOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "ShowOrder",
                table: "Collections");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Collections",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Platformes",
                columns: new[] { "ID", "CodeName", "Databases", "IgdbId", "LUProfileID", "Name" },
                values: new object[,]
                {
                    { new Guid("3b24e0f9-50e3-457d-bd90-64e5fb06e20f"), "Epic", "", null, null, "Epic Games Store" },
                    { new Guid("853358e9-071e-40e8-b185-d47112ff9350"), "EA Play", "", null, null, "EA Origin" },
                    { new Guid("8557d4a3-f172-4af5-b38a-d958d1f2339a"), "Steam", "", null, null, "Steam" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("3b24e0f9-50e3-457d-bd90-64e5fb06e20f"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("853358e9-071e-40e8-b185-d47112ff9350"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("8557d4a3-f172-4af5-b38a-d958d1f2339a"));

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Collections");

            migrationBuilder.AddColumn<string>(
                name: "ShowOrder",
                table: "Collections",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

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
    }
}
