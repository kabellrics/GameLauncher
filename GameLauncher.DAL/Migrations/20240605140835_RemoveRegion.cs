using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLauncher.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Regions_LURegionId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Items_LURegionId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Dev",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Editeur",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "LURegionId",
                table: "Items");

            migrationBuilder.CreateTable(
                name: "Develloppeurs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ItemID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Develloppeurs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Develloppeurs_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Editeurs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ItemID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editeurs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Editeurs_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Develloppeurs_ItemID",
                table: "Develloppeurs",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Editeurs_ItemID",
                table: "Editeurs",
                column: "ItemID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Develloppeurs");

            migrationBuilder.DropTable(
                name: "Editeurs");

            migrationBuilder.AddColumn<string>(
                name: "Dev",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Editeur",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "LURegionId",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_LURegionId",
                table: "Items",
                column: "LURegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Regions_LURegionId",
                table: "Items",
                column: "LURegionId",
                principalTable: "Regions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
