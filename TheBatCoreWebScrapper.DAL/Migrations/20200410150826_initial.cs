using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBatCoreWebScrapper.DAL.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrlLibraries",
                columns: table => new
                {
                    UrlLibraryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlLibraries", x => x.UrlLibraryId);
                });

            migrationBuilder.CreateTable(
                name: "ScrappingResults",
                columns: table => new
                {
                    ScrappingResultId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyResult = table.Column<string>(nullable: true),
                    UrlLibraryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrappingResults", x => x.ScrappingResultId);
                    table.ForeignKey(
                        name: "FK_ScrappingResults_UrlLibraries_UrlLibraryId",
                        column: x => x.UrlLibraryId,
                        principalTable: "UrlLibraries",
                        principalColumn: "UrlLibraryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScrappingResults_UrlLibraryId",
                table: "ScrappingResults",
                column: "UrlLibraryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScrappingResults");

            migrationBuilder.DropTable(
                name: "UrlLibraries");
        }
    }
}
