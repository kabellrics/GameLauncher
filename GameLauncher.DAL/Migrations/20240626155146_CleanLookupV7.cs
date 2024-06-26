using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLauncher.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CleanLookupV7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "Platformes",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Platformes",
                newName: "Guid");
        }
    }
}
