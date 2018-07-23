using Microsoft.EntityFrameworkCore.Migrations;

namespace KentNoteBook.Data.Migrations
{
    public partial class Add_SystemRole_Description_IsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SystemRole",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "SystemRole",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "SystemRole");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "SystemRole");
        }
    }
}
