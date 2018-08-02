using Microsoft.EntityFrameworkCore.Migrations;

namespace KentNoteBook.Data.Migrations
{
    public partial class Add_SystemPermission_Code : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "SystemPermission",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SystemMenu_ParentId",
                table: "SystemMenu",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemMenu_SystemMenu_ParentId",
                table: "SystemMenu",
                column: "ParentId",
                principalTable: "SystemMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemMenu_SystemMenu_ParentId",
                table: "SystemMenu");

            migrationBuilder.DropIndex(
                name: "IX_SystemMenu_ParentId",
                table: "SystemMenu");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "SystemPermission");
        }
    }
}
