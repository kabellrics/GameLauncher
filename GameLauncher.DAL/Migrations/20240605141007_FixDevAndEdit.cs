using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLauncher.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixDevAndEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Develloppeurs_Items_ItemID",
                table: "Develloppeurs");

            migrationBuilder.DropForeignKey(
                name: "FK_Editeurs_Items_ItemID",
                table: "Editeurs");

            migrationBuilder.DropIndex(
                name: "IX_Editeurs_ItemID",
                table: "Editeurs");

            migrationBuilder.DropIndex(
                name: "IX_Develloppeurs_ItemID",
                table: "Develloppeurs");

            migrationBuilder.DropColumn(
                name: "ItemID",
                table: "Editeurs");

            migrationBuilder.DropColumn(
                name: "ItemID",
                table: "Develloppeurs");

            migrationBuilder.CreateTable(
                name: "DevelloppeurItem",
                columns: table => new
                {
                    DevelloppeursID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemsID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevelloppeurItem", x => new { x.DevelloppeursID, x.ItemsID });
                    table.ForeignKey(
                        name: "FK_DevelloppeurItem_Develloppeurs_DevelloppeursID",
                        column: x => x.DevelloppeursID,
                        principalTable: "Develloppeurs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DevelloppeurItem_Items_ItemsID",
                        column: x => x.ItemsID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EditeurItem",
                columns: table => new
                {
                    EditeursID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemsID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditeurItem", x => new { x.EditeursID, x.ItemsID });
                    table.ForeignKey(
                        name: "FK_EditeurItem_Editeurs_EditeursID",
                        column: x => x.EditeursID,
                        principalTable: "Editeurs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EditeurItem_Items_ItemsID",
                        column: x => x.ItemsID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevelloppeurItem_ItemsID",
                table: "DevelloppeurItem",
                column: "ItemsID");

            migrationBuilder.CreateIndex(
                name: "IX_EditeurItem_ItemsID",
                table: "EditeurItem",
                column: "ItemsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DevelloppeurItem");

            migrationBuilder.DropTable(
                name: "EditeurItem");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemID",
                table: "Editeurs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ItemID",
                table: "Develloppeurs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Editeurs_ItemID",
                table: "Editeurs",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Develloppeurs_ItemID",
                table: "Develloppeurs",
                column: "ItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Develloppeurs_Items_ItemID",
                table: "Develloppeurs",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Editeurs_Items_ItemID",
                table: "Editeurs",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ID");
        }
    }
}
