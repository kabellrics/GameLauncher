using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLauncher.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CleanLookupV6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CodeName = table.Column<string>(type: "TEXT", nullable: false),
                    Fanart = table.Column<string>(type: "TEXT", nullable: false),
                    Logo = table.Column<string>(type: "TEXT", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Develloppeurs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Develloppeurs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Editeurs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editeurs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Emulateurs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Website = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    LUPlatformesId = table.Column<string>(type: "TEXT", nullable: false),
                    LUProfileId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SearchName = table.Column<string>(type: "TEXT", nullable: false),
                    Path = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    IsFavorite = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AddingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Cover = table.Column<string>(type: "TEXT", nullable: false),
                    Logo = table.Column<string>(type: "TEXT", nullable: false),
                    Banner = table.Column<string>(type: "TEXT", nullable: false),
                    Artwork = table.Column<string>(type: "TEXT", nullable: false),
                    Video = table.Column<string>(type: "TEXT", nullable: false),
                    StoreId = table.Column<string>(type: "TEXT", nullable: false),
                    NbStart = table.Column<int>(type: "INTEGER", nullable: false),
                    LastStartDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Platformes",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Codename = table.Column<string>(type: "TEXT", nullable: false),
                    IgdbId = table.Column<long>(type: "INTEGER", nullable: true),
                    Databases = table.Column<string>(type: "TEXT", nullable: false),
                    Emulators = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    StartupArguments = table.Column<string>(type: "TEXT", nullable: false),
                    Platforms = table.Column<string>(type: "TEXT", nullable: false),
                    ImageExtensions = table.Column<string>(type: "TEXT", nullable: false),
                    ProfileFiles = table.Column<string>(type: "TEXT", nullable: false),
                    StartupExecutable = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                });

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
                    DevelloppeurID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemID = table.Column<Guid>(type: "TEXT", nullable: false)
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
                name: "Emulateurs");

            migrationBuilder.DropTable(
                name: "GenredItems");

            migrationBuilder.DropTable(
                name: "Platformes");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "Develloppeurs");

            migrationBuilder.DropTable(
                name: "Editeurs");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
