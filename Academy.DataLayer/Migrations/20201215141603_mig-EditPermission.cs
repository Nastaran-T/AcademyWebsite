using Microsoft.EntityFrameworkCore.Migrations;

namespace Academy.DataLayer.Migrations
{
    public partial class migEditPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Permissions_PermissionId1",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_PermissionId1",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "PermissionId1",
                table: "Permissions");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ParentId",
                table: "Permissions",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Permissions_ParentId",
                table: "Permissions",
                column: "ParentId",
                principalTable: "Permissions",
                principalColumn: "PermissionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Permissions_ParentId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_ParentId",
                table: "Permissions");

            migrationBuilder.AddColumn<int>(
                name: "PermissionId1",
                table: "Permissions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionId1",
                table: "Permissions",
                column: "PermissionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Permissions_PermissionId1",
                table: "Permissions",
                column: "PermissionId1",
                principalTable: "Permissions",
                principalColumn: "PermissionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
