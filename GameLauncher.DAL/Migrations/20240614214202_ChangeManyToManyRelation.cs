using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameLauncher.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeManyToManyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectionItem");

            migrationBuilder.DropTable(
                name: "DevelloppeurItem");

            migrationBuilder.DropTable(
                name: "EditeurItem");

            migrationBuilder.DropTable(
                name: "GenreItem");

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("42d3de15-0041-4a26-a4e3-06e34f029a70"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("9cfb55d2-e47f-4cb3-b2c0-65789cdfe620"));

            migrationBuilder.DeleteData(
                table: "Platformes",
                keyColumn: "ID",
                keyValue: new Guid("fc042e25-0ea5-4db2-b242-7aa4c60165de"));

            migrationBuilder.CreateTable(
                name: "CollectiondItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    CollectionID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectiondItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CollectiondItems_Collections_CollectionID",
                        column: x => x.CollectionID,
                        principalTable: "Collections",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectiondItems_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DevdItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    DevID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemID = table.Column<Guid>(type: "TEXT", nullable: false),
                    DevelloppeurID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevdItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DevdItems_Develloppeurs_DevelloppeurID",
                        column: x => x.DevelloppeurID,
                        principalTable: "Develloppeurs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DevdItems_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EditeurdItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    EditeurID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditeurdItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EditeurdItems_Editeurs_EditeurID",
                        column: x => x.EditeurID,
                        principalTable: "Editeurs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EditeurdItems_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenredItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    GenreID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenredItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GenredItems_Genres_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenredItems_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Platformes",
                columns: new[] { "ID", "CodeName", "Databases", "IgdbId", "LUProfileID", "Name" },
                values: new object[,]
                {
                    { new Guid("4d230b99-fb69-4e93-9e7a-9215c3bc45b1"), "Epic", "", null, null, "Epic Games Store" },
                    { new Guid("bce4011a-b705-4d18-993d-c8d81fec3c66"), "Steam", "", null, null, "Steam" },
                    { new Guid("dbe21f56-6e46-46b6-8c5b-8e565f68b10e"), "EA Play", "", null, null, "EA Origin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectiondItems_CollectionID",
                table: "CollectiondItems",
                column: "CollectionID");

            migrationBuilder.CreateIndex(
                name: "IX_CollectiondItems_ItemID",
                table: "CollectiondItems",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_DevdItems_DevelloppeurID",
                table: "DevdItems",
                column: "DevelloppeurID");

            migrationBuilder.CreateIndex(
                name: "IX_DevdItems_ItemID",
                table: "DevdItems",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_EditeurdItems_EditeurID",
                table: "EditeurdItems",
                column: "EditeurID");

            migrationBuilder.CreateIndex(
                name: "IX_EditeurdItems_ItemID",
                table: "EditeurdItems",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_GenredItems_GenreID",
                table: "GenredItems",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_GenredItems_ItemID",
                table: "GenredItems",
                column: "ItemID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectiondItems");

            migrationBuilder.DropTable(
                name: "DevdItems");

            migrationBuilder.DropTable(
                name: "EditeurdItems");

            migrationBuilder.DropTable(
                name: "GenredItems");

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

            migrationBuilder.CreateTable(
                name: "CollectionItem",
                columns: table => new
                {
                    CollectionsID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemsID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionItem", x => new { x.CollectionsID, x.ItemsID });
                    table.ForeignKey(
                        name: "FK_CollectionItem_Collections_CollectionsID",
                        column: x => x.CollectionsID,
                        principalTable: "Collections",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionItem_Items_ItemsID",
                        column: x => x.ItemsID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "GenreItem",
                columns: table => new
                {
                    GenresID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemsID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreItem", x => new { x.GenresID, x.ItemsID });
                    table.ForeignKey(
                        name: "FK_GenreItem_Genres_GenresID",
                        column: x => x.GenresID,
                        principalTable: "Genres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreItem_Items_ItemsID",
                        column: x => x.ItemsID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Platformes",
                columns: new[] { "ID", "CodeName", "Databases", "IgdbId", "LUProfileID", "Name" },
                values: new object[,]
                {
                    { new Guid("42d3de15-0041-4a26-a4e3-06e34f029a70"), "EA Play", "", null, null, "EA Origin" },
                    { new Guid("9cfb55d2-e47f-4cb3-b2c0-65789cdfe620"), "Steam", "", null, null, "Steam" },
                    { new Guid("fc042e25-0ea5-4db2-b242-7aa4c60165de"), "Epic", "", null, null, "Epic Games Store" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItem_ItemsID",
                table: "CollectionItem",
                column: "ItemsID");

            migrationBuilder.CreateIndex(
                name: "IX_DevelloppeurItem_ItemsID",
                table: "DevelloppeurItem",
                column: "ItemsID");

            migrationBuilder.CreateIndex(
                name: "IX_EditeurItem_ItemsID",
                table: "EditeurItem",
                column: "ItemsID");

            migrationBuilder.CreateIndex(
                name: "IX_GenreItem_ItemsID",
                table: "GenreItem",
                column: "ItemsID");
        }
    }
}
