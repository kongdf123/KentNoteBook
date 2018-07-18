using KentNoteBook.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KentNoteBook.Data
{
	partial class KentNoteBookDbContext : DbContext
	{
		// System
		public DbSet<SystemMenu> Menus { get; set; }
		public DbSet<SystemOperation> Operations { get; set; }
		public DbSet<SystemPermission> Permissions { get; set; }
		public DbSet<SystemRole> Roles { get; set; }
		public DbSet<SystemUser> Users { get; set; }
		public DbSet<SystemUserGroup> UserGroups { get; set; }
		public DbSet<UsersInUserGroup> UsersInUserGroups { get; set; }
		public DbSet<MenusInRole> ModulesInRoles { get; set; }
		public DbSet<UsersInRole> UsersInRoles { get; set; }
		public DbSet<PermissionsInMenu> PermissionsInMenus { get; set; }
		public DbSet<OperationsInPermission> OperationsInPermissions { get; set; }

	}
}
