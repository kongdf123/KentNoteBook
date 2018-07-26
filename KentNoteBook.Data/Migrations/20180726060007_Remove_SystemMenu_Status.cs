using Microsoft.EntityFrameworkCore.Migrations;

namespace KentNoteBook.Data.Migrations
{
    public partial class Remove_SystemMenu_Status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "SystemMenu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "SystemMenu",
                nullable: false,
                defaultValue: 0);
        }
    }
}
