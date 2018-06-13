using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LinkHolder.Migrations
{
    public partial class CustomProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FolderId",
                table: "Links",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Folder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Folder_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Links_FolderId",
                table: "Links",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Folder_AppUserId",
                table: "Folder",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Folder_FolderId",
                table: "Links",
                column: "FolderId",
                principalTable: "Folder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Folder_FolderId",
                table: "Links");

            migrationBuilder.DropTable(
                name: "Folder");

            migrationBuilder.DropIndex(
                name: "IX_Links_FolderId",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "FolderId",
                table: "Links");
        }
    }
}
