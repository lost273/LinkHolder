using Microsoft.EntityFrameworkCore.Migrations;

namespace LinkHolder.Migrations
{
    public partial class CustomProperties2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folder_AspNetUsers_AppUserId",
                table: "Folder");

            migrationBuilder.DropForeignKey(
                name: "FK_Links_Folder_FolderId",
                table: "Links");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Folder",
                table: "Folder");

            migrationBuilder.RenameTable(
                name: "Folder",
                newName: "Folders");

            migrationBuilder.RenameIndex(
                name: "IX_Folder_AppUserId",
                table: "Folders",
                newName: "IX_Folders_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Folders",
                table: "Folders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_AspNetUsers_AppUserId",
                table: "Folders",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Folders_FolderId",
                table: "Links",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_AspNetUsers_AppUserId",
                table: "Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Links_Folders_FolderId",
                table: "Links");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Folders",
                table: "Folders");

            migrationBuilder.RenameTable(
                name: "Folders",
                newName: "Folder");

            migrationBuilder.RenameIndex(
                name: "IX_Folders_AppUserId",
                table: "Folder",
                newName: "IX_Folder_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Folder",
                table: "Folder",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Folder_AspNetUsers_AppUserId",
                table: "Folder",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Folder_FolderId",
                table: "Links",
                column: "FolderId",
                principalTable: "Folder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
