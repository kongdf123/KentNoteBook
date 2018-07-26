using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KentNoteBook.Data.Migrations
{
    public partial class Add_SystemMenu_ParentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "SystemMenu");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "SystemMenu",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "SystemMenu");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "SystemMenu",
                nullable: false,
                defaultValue: 0);
        }
    }
}
