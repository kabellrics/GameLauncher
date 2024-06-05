using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameLauncher.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIDForLuPlatforms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Platformes_PlatformesID",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_PlatformesID",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "PlatformesID",
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

            migrationBuilder.CreateIndex(
                name: "IX_Items_LUPlatformesId",
                table: "Items",
                column: "LUPlatformesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Platformes_LUPlatformesId",
                table: "Items",
                column: "LUPlatformesId",
                principalTable: "Platformes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Platformes_LUPlatformesId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_LUPlatformesId",
                table: "Items");

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

            migrationBuilder.AddColumn<string>(
                name: "PlatformesID",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_PlatformesID",
                table: "Items",
                column: "PlatformesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Platformes_PlatformesID",
                table: "Items",
                column: "PlatformesID",
                principalTable: "Platformes",
                principalColumn: "ID");
        }
    }
}
