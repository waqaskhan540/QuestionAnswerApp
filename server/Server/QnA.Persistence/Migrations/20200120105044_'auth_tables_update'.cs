using Microsoft.EntityFrameworkCore.Migrations;

namespace QnA.Persistence.Migrations
{
    public partial class auth_tables_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperApps_Users_DeveloperId",
                table: "DeveloperApps");

            migrationBuilder.DropIndex(
                name: "IX_DeveloperApps_DeveloperId",
                table: "DeveloperApps");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "DeveloperApps");

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperApps_UserId",
                table: "DeveloperApps",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperApps_Users_UserId",
                table: "DeveloperApps",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperApps_Users_UserId",
                table: "DeveloperApps");

            migrationBuilder.DropIndex(
                name: "IX_DeveloperApps_UserId",
                table: "DeveloperApps");

            migrationBuilder.AddColumn<int>(
                name: "DeveloperId",
                table: "DeveloperApps",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperApps_DeveloperId",
                table: "DeveloperApps",
                column: "DeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperApps_Users_DeveloperId",
                table: "DeveloperApps",
                column: "DeveloperId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
