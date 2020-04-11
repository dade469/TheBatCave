using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBatCoreWebScrapper.DAL.Migrations
{
    public partial class AddScrapperConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScrappingResults_UrlLibraries_UrlLibraryId",
                table: "ScrappingResults");

            migrationBuilder.DropIndex(
                name: "IX_ScrappingResults_UrlLibraryId",
                table: "ScrappingResults");

            migrationBuilder.DropColumn(
                name: "UrlLibraryId",
                table: "ScrappingResults");

            migrationBuilder.AddColumn<int>(
                name: "ConfigurationScrappingConfigurationId",
                table: "ScrappingResults",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ScrappingConfigurations",
                columns: table => new
                {
                    ScrappingConfigurationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlLibraryId = table.Column<int>(nullable: true),
                    Interval = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrappingConfigurations", x => x.ScrappingConfigurationId);
                    table.ForeignKey(
                        name: "FK_ScrappingConfigurations_UrlLibraries_UrlLibraryId",
                        column: x => x.UrlLibraryId,
                        principalTable: "UrlLibraries",
                        principalColumn: "UrlLibraryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScrappingResults_ConfigurationScrappingConfigurationId",
                table: "ScrappingResults",
                column: "ConfigurationScrappingConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ScrappingConfigurations_UrlLibraryId",
                table: "ScrappingConfigurations",
                column: "UrlLibraryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScrappingResults_ScrappingConfigurations_ConfigurationScrappingConfigurationId",
                table: "ScrappingResults",
                column: "ConfigurationScrappingConfigurationId",
                principalTable: "ScrappingConfigurations",
                principalColumn: "ScrappingConfigurationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScrappingResults_ScrappingConfigurations_ConfigurationScrappingConfigurationId",
                table: "ScrappingResults");

            migrationBuilder.DropTable(
                name: "ScrappingConfigurations");

            migrationBuilder.DropIndex(
                name: "IX_ScrappingResults_ConfigurationScrappingConfigurationId",
                table: "ScrappingResults");

            migrationBuilder.DropColumn(
                name: "ConfigurationScrappingConfigurationId",
                table: "ScrappingResults");

            migrationBuilder.AddColumn<int>(
                name: "UrlLibraryId",
                table: "ScrappingResults",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScrappingResults_UrlLibraryId",
                table: "ScrappingResults",
                column: "UrlLibraryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScrappingResults_UrlLibraries_UrlLibraryId",
                table: "ScrappingResults",
                column: "UrlLibraryId",
                principalTable: "UrlLibraries",
                principalColumn: "UrlLibraryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
