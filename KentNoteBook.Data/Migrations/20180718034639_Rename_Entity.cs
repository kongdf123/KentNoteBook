using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KentNoteBook.Data.Migrations
{
    public partial class Rename_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenusInRole_Menu_MenuId",
                table: "MenusInRole");

            migrationBuilder.DropForeignKey(
                name: "FK_MenusInRole_Role_RoleId",
                table: "MenusInRole");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationsInPermission_Operation_OperationId",
                table: "OperationsInPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationsInPermission_Permission_PermissionId",
                table: "OperationsInPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionsInMenu_Menu_MenuId",
                table: "PermissionsInMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionsInMenu_Permission_PermissionId",
                table: "PermissionsInMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_RolesInUserGroup_Role_RoleId",
                table: "RolesInUserGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_RolesInUserGroup_UserGroup_UserGroupId",
                table: "RolesInUserGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInRole_Role_RoleId",
                table: "UsersInRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInRole_User_UserId",
                table: "UsersInRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInUserGroup_UserGroup_UserGroupId",
                table: "UsersInUserGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInUserGroup_User_UserId",
                table: "UsersInUserGroup");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.CreateTable(
                name: "SystemMenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemOperation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemOperation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemPermission",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PermissionType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemPermission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemRole",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemUser",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    NickName = table.Column<string>(maxLength: 30, nullable: true),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    PasswordSalt = table.Column<string>(maxLength: 30, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Mobile = table.Column<string>(maxLength: 30, nullable: true),
                    Avatar = table.Column<string>(maxLength: 150, nullable: true),
                    Discription = table.Column<string>(maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemUserGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUserGroup", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MenusInRole_SystemMenu_MenuId",
                table: "MenusInRole",
                column: "MenuId",
                principalTable: "SystemMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenusInRole_SystemRole_RoleId",
                table: "MenusInRole",
                column: "RoleId",
                principalTable: "SystemRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsInPermission_SystemOperation_OperationId",
                table: "OperationsInPermission",
                column: "OperationId",
                principalTable: "SystemOperation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsInPermission_SystemPermission_PermissionId",
                table: "OperationsInPermission",
                column: "PermissionId",
                principalTable: "SystemPermission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionsInMenu_SystemMenu_MenuId",
                table: "PermissionsInMenu",
                column: "MenuId",
                principalTable: "SystemMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionsInMenu_SystemPermission_PermissionId",
                table: "PermissionsInMenu",
                column: "PermissionId",
                principalTable: "SystemPermission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolesInUserGroup_SystemRole_RoleId",
                table: "RolesInUserGroup",
                column: "RoleId",
                principalTable: "SystemRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolesInUserGroup_SystemUserGroup_UserGroupId",
                table: "RolesInUserGroup",
                column: "UserGroupId",
                principalTable: "SystemUserGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInRole_SystemRole_RoleId",
                table: "UsersInRole",
                column: "RoleId",
                principalTable: "SystemRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInRole_SystemUser_UserId",
                table: "UsersInRole",
                column: "UserId",
                principalTable: "SystemUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInUserGroup_SystemUserGroup_UserGroupId",
                table: "UsersInUserGroup",
                column: "UserGroupId",
                principalTable: "SystemUserGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInUserGroup_SystemUser_UserId",
                table: "UsersInUserGroup",
                column: "UserId",
                principalTable: "SystemUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenusInRole_SystemMenu_MenuId",
                table: "MenusInRole");

            migrationBuilder.DropForeignKey(
                name: "FK_MenusInRole_SystemRole_RoleId",
                table: "MenusInRole");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationsInPermission_SystemOperation_OperationId",
                table: "OperationsInPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationsInPermission_SystemPermission_PermissionId",
                table: "OperationsInPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionsInMenu_SystemMenu_MenuId",
                table: "PermissionsInMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionsInMenu_SystemPermission_PermissionId",
                table: "PermissionsInMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_RolesInUserGroup_SystemRole_RoleId",
                table: "RolesInUserGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_RolesInUserGroup_SystemUserGroup_UserGroupId",
                table: "RolesInUserGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInRole_SystemRole_RoleId",
                table: "UsersInRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInRole_SystemUser_UserId",
                table: "UsersInRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInUserGroup_SystemUserGroup_UserGroupId",
                table: "UsersInUserGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInUserGroup_SystemUser_UserId",
                table: "UsersInUserGroup");

            migrationBuilder.DropTable(
                name: "SystemMenu");

            migrationBuilder.DropTable(
                name: "SystemOperation");

            migrationBuilder.DropTable(
                name: "SystemPermission");

            migrationBuilder.DropTable(
                name: "SystemRole");

            migrationBuilder.DropTable(
                name: "SystemUser");

            migrationBuilder.DropTable(
                name: "SystemUserGroup");

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PermissionType = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Avatar = table.Column<string>(maxLength: 150, nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Discription = table.Column<string>(maxLength: 500, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Mobile = table.Column<string>(maxLength: 30, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    NickName = table.Column<string>(maxLength: 30, nullable: true),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    PasswordSalt = table.Column<string>(maxLength: 30, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MenusInRole_Menu_MenuId",
                table: "MenusInRole",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenusInRole_Role_RoleId",
                table: "MenusInRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsInPermission_Operation_OperationId",
                table: "OperationsInPermission",
                column: "OperationId",
                principalTable: "Operation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsInPermission_Permission_PermissionId",
                table: "OperationsInPermission",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionsInMenu_Menu_MenuId",
                table: "PermissionsInMenu",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionsInMenu_Permission_PermissionId",
                table: "PermissionsInMenu",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolesInUserGroup_Role_RoleId",
                table: "RolesInUserGroup",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolesInUserGroup_UserGroup_UserGroupId",
                table: "RolesInUserGroup",
                column: "UserGroupId",
                principalTable: "UserGroup",
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

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInUserGroup_UserGroup_UserGroupId",
                table: "UsersInUserGroup",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInUserGroup_User_UserId",
                table: "UsersInUserGroup",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
