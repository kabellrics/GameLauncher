using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLauncher.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    ShowOrder = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.ID);
                });

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
                });

            migrationBuilder.CreateTable(
                name: "Emulateurs",
                columns: table => new
                {
                    ID = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Website = table.Column<string>(type: "TEXT", nullable: false),
                    LUPlatformesID = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emulateurs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LUEmulateurId = table.Column<string>(type: "TEXT", nullable: false),
                    StartupArguments = table.Column<string>(type: "TEXT", nullable: false),
                    ImageExtensions = table.Column<string>(type: "TEXT", nullable: false),
                    ProfileFiles = table.Column<string>(type: "TEXT", nullable: false),
                    StartupExecutable = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Profiles_Emulateurs_LUEmulateurId",
                        column: x => x.LUEmulateurId,
                        principalTable: "Emulateurs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Platformes",
                columns: table => new
                {
                    ID = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CodeName = table.Column<string>(type: "TEXT", nullable: false),
                    IgdbId = table.Column<long>(type: "INTEGER", nullable: true),
                    Databases = table.Column<string>(type: "TEXT", nullable: false),
                    LUProfileID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platformes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Platformes_Profiles_LUProfileID",
                        column: x => x.LUProfileID,
                        principalTable: "Profiles",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    LUPlatformesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LUProfileId = table.Column<Guid>(type: "TEXT", nullable: true),
                    PlatformesID = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SearchName = table.Column<string>(type: "TEXT", nullable: false),
                    Path = table.Column<string>(type: "TEXT", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Dev = table.Column<string>(type: "TEXT", nullable: false),
                    Editeur = table.Column<string>(type: "TEXT", nullable: false),
                    LURegionId = table.Column<Guid>(type: "TEXT", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Items_Platformes_PlatformesID",
                        column: x => x.PlatformesID,
                        principalTable: "Platformes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Items_Profiles_LUProfileId",
                        column: x => x.LUProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Items_Regions_LURegionId",
                        column: x => x.LURegionId,
                        principalTable: "Regions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ItemID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Genres_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "MetadataGenres",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    GenreId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataGenres", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MetadataGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItem_ItemsID",
                table: "CollectionItem",
                column: "ItemsID");

            migrationBuilder.CreateIndex(
                name: "IX_Emulateurs_LUPlatformesID",
                table: "Emulateurs",
                column: "LUPlatformesID");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_ItemID",
                table: "Genres",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_LUProfileId",
                table: "Items",
                column: "LUProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_LURegionId",
                table: "Items",
                column: "LURegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_PlatformesID",
                table: "Items",
                column: "PlatformesID");

            migrationBuilder.CreateIndex(
                name: "IX_MetadataGenres_GenreId",
                table: "MetadataGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Platformes_LUProfileID",
                table: "Platformes",
                column: "LUProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_LUEmulateurId",
                table: "Profiles",
                column: "LUEmulateurId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionItem_Items_ItemsID",
                table: "CollectionItem",
                column: "ItemsID",
                principalTable: "Items",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Emulateurs_Platformes_LUPlatformesID",
                table: "Emulateurs",
                column: "LUPlatformesID",
                principalTable: "Platformes",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emulateurs_Platformes_LUPlatformesID",
                table: "Emulateurs");

            migrationBuilder.DropTable(
                name: "CollectionItem");

            migrationBuilder.DropTable(
                name: "MetadataGenres");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Platformes");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Emulateurs");
        }
    }
}
