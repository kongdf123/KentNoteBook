using KentNoteBook.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KentNoteBook.Data
{
	partial class KentNoteBookDbContext : DbContext
	{
		// System
		public DbSet<Menu> Menus { get; set; }
		public DbSet<Operation> Operations { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserGroup> UserGroups { get; set; }
		public DbSet<UsersInUserGroup> UsersInUserGroups { get; set; }
		public DbSet<MenusInRole> ModulesInRoles { get; set; }
		public DbSet<UsersInRole> UsersInRoles { get; set; }
		public DbSet<PermissionsInMenu> PermissionsInMenus { get; set; }
		public DbSet<OperationsInPermission> OperationsInPermissions { get; set; }

	}
}
