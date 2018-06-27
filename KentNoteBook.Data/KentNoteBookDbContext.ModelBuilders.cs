using KentNoteBook.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace KentNoteBook.Data
{
	partial class KentNoteBookDbContext : DbContext
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder) {

			modelBuilder.Entity<UserGroup>(map => {
				map.HasMany(x => x.UsersInUserGroups).WithOne(x => x.UserGroup).HasForeignKey(x => x.UserGroupId);
				map.HasMany(x => x.RolesInUserGroups).WithOne(x => x.UserGroup).HasForeignKey(x => x.UserGroupId);
			});
			modelBuilder.Entity<UsersInUserGroup>(map => {
				map.HasKey(x => new { x.UserId, x.UserGroupId });
			});
			modelBuilder.Entity<RolesInUserGroup>(map => {
				map.HasKey(x => new { x.RoleId, x.UserGroupId });
			});

			modelBuilder.Entity<Role>(map => {
				map.HasMany(x => x.ModulesInRoles).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
				map.HasMany(x => x.UsersInRoles).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
			});
			modelBuilder.Entity<MenusInRole>(map => {
				map.HasKey(x => new { x.RoleId, x.MenuId });
			});
			modelBuilder.Entity<UsersInRole>(map => {
				map.HasKey(x => new { x.UserId, x.RoleId });
			});


			modelBuilder.Entity<Menu>(map => {
				map.HasMany(x => x.PermissionsInMenus).WithOne(x => x.Menu).HasForeignKey(x => x.MenuId);
			});
			modelBuilder.Entity<PermissionsInMenu>(map => {
				map.HasKey(x => new { x.PermissionId, x.MenuId });
			});

			modelBuilder.Entity<Permission>(map => {
				map.HasMany(x => x.OperationsInPermissions).WithOne(x => x.Permission).HasForeignKey(x => x.PermissionId);
			});
			modelBuilder.Entity<OperationsInPermission>(map => {
				map.HasKey(x => new { x.PermissionId, x.OperationId });
			});

			modelBuilder.RemovePluralizingTableNameConvention();

		}
	}

	public static class ModelBuilderExtensions
	{
		public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder) {
			foreach ( IMutableEntityType entity in modelBuilder.Model.GetEntityTypes() ) {
				entity.Relational().TableName = entity.DisplayName();
			}
		}
	}
}
