using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QnA.Persistence.Migrations
{
    public partial class auth_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeveloperApps",
                columns: table => new
                {
                    AppId = table.Column<Guid>(nullable: false),
                    AppName = table.Column<string>(maxLength: 100, nullable: false),
                    RequiresConsent = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    DeveloperId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperApps", x => x.AppId);
                    table.ForeignKey(
                        name: "FK_DeveloperApps_Users_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RedirectUrls",
                columns: table => new
                {
                    AppId = table.Column<Guid>(nullable: false),
                    RedirectUri = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedirectUrls", x => new { x.AppId, x.RedirectUri });
                    table.ForeignKey(
                        name: "FK_RedirectUrls_DeveloperApps_AppId",
                        column: x => x.AppId,
                        principalTable: "DeveloperApps",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperApps_DeveloperId",
                table: "DeveloperApps",
                column: "DeveloperId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RedirectUrls");

            migrationBuilder.DropTable(
                name: "DeveloperApps");
        }
    }
}
