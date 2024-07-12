using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLauncher.DAL.Migrations
{
    /// <inheritdoc />
    public partial class LocalEMuAndProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Emulateurs",
                table: "Emulateurs");

            migrationBuilder.RenameTable(
                name: "Profiles",
                newName: "LUProfile");

            migrationBuilder.RenameTable(
                name: "Emulateurs",
                newName: "LUEmulateur");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LUProfile",
                table: "LUProfile",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LUEmulateur",
                table: "LUEmulateur",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LUProfile",
                table: "LUProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LUEmulateur",
                table: "LUEmulateur");

            migrationBuilder.RenameTable(
                name: "LUProfile",
                newName: "Profiles");

            migrationBuilder.RenameTable(
                name: "LUEmulateur",
                newName: "Emulateurs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Emulateurs",
                table: "Emulateurs",
                column: "Id");
        }
    }
}
