using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLauncher.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixEmuProfil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LUEmulateurId",
                table: "Profiles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Profiles",
                table: "Emulateurs",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Platformes",
                table: "Platformes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Emulateurs",
                table: "Emulateurs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Platformes",
                table: "Platformes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Emulateurs",
                table: "Emulateurs");

            migrationBuilder.DropColumn(
                name: "LUEmulateurId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Profiles",
                table: "Emulateurs");
        }
    }
}
