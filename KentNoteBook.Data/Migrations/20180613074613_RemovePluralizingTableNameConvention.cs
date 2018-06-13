using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KentNoteBook.Data.Migrations
{
    public partial class RemovePluralizingTableNameConvention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModulesInRoles_Roles_RoleId",
                table: "ModulesInRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ModulesInRoles_SystemModules_SystemModuleId",
                table: "ModulesInRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemActions_SystemModules_SystemModuleId",
                table: "SystemActions");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInRoles_Roles_RoleId",
                table: "UsersInRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInRoles_Users_UserId",
                table: "UsersInRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersInRoles",
                table: "UsersInRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemModules",
                table: "SystemModules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemActions",
                table: "SystemActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModulesInRoles",
                table: "ModulesInRoles");

            migrationBuilder.RenameTable(
                name: "UsersInRoles",
                newName: "UsersInRole");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "SystemModules",
                newName: "SystemModule");

            migrationBuilder.RenameTable(
                name: "SystemActions",
                newName: "SystemAction");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "ModulesInRoles",
                newName: "ModulesInRole");

            migrationBuilder.RenameIndex(
                name: "IX_UsersInRoles_RoleId",
                table: "UsersInRole",
                newName: "IX_UsersInRole_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_SystemActions_SystemModuleId",
                table: "SystemAction",
                newName: "IX_SystemAction_SystemModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_ModulesInRoles_SystemModuleId",
                table: "ModulesInRole",
                newName: "IX_ModulesInRole_SystemModuleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersInRole",
                table: "UsersInRole",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemModule",
                table: "SystemModule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemAction",
                table: "SystemAction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModulesInRole",
                table: "ModulesInRole",
                columns: new[] { "RoleId", "SystemModuleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ModulesInRole_Role_RoleId",
                table: "ModulesInRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModulesInRole_SystemModule_SystemModuleId",
                table: "ModulesInRole",
                column: "SystemModuleId",
                principalTable: "SystemModule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemAction_SystemModule_SystemModuleId",
                table: "SystemAction",
                column: "SystemModuleId",
                principalTable: "SystemModule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInRole_Role_RoleId",
                table: "UsersInRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInRole_User_UserId",
                table: "UsersInRole",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModulesInRole_Role_RoleId",
                table: "ModulesInRole");

            migrationBuilder.DropForeignKey(
                name: "FK_ModulesInRole_SystemModule_SystemModuleId",
                table: "ModulesInRole");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemAction_SystemModule_SystemModuleId",
                table: "SystemAction");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInRole_Role_RoleId",
                table: "UsersInRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInRole_User_UserId",
                table: "UsersInRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersInRole",
                table: "UsersInRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemModule",
                table: "SystemModule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemAction",
                table: "SystemAction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModulesInRole",
                table: "ModulesInRole");

            migrationBuilder.RenameTable(
                name: "UsersInRole",
                newName: "UsersInRoles");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "SystemModule",
                newName: "SystemModules");

            migrationBuilder.RenameTable(
                name: "SystemAction",
                newName: "SystemActions");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "ModulesInRole",
                newName: "ModulesInRoles");

            migrationBuilder.RenameIndex(
                name: "IX_UsersInRole_RoleId",
                table: "UsersInRoles",
                newName: "IX_UsersInRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_SystemAction_SystemModuleId",
                table: "SystemActions",
                newName: "IX_SystemActions_SystemModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_ModulesInRole_SystemModuleId",
                table: "ModulesInRoles",
                newName: "IX_ModulesInRoles_SystemModuleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersInRoles",
                table: "UsersInRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemModules",
                table: "SystemModules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemActions",
                table: "SystemActions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModulesInRoles",
                table: "ModulesInRoles",
                columns: new[] { "RoleId", "SystemModuleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ModulesInRoles_Roles_RoleId",
                table: "ModulesInRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModulesInRoles_SystemModules_SystemModuleId",
                table: "ModulesInRoles",
                column: "SystemModuleId",
                principalTable: "SystemModules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemActions_SystemModules_SystemModuleId",
                table: "SystemActions",
                column: "SystemModuleId",
                principalTable: "SystemModules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInRoles_Roles_RoleId",
                table: "UsersInRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInRoles_Users_UserId",
                table: "UsersInRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
