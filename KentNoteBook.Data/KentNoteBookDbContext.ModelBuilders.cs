using KentNoteBook.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace KentNoteBook.Data
{
	partial class KentNoteBookDbContext : DbContext
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder) {

			modelBuilder.Entity<UsersInRole>(map => {
				map.HasKey(x => new { x.UserId, x.RoleId });
				map.HasOne(x => x.Role).WithMany(x => x.UsersInRoles).HasForeignKey(x => x.RoleId);
				map.HasOne(x => x.User).WithMany(x => x.UsersInRoles).HasForeignKey(x => x.UserId);
			});

			modelBuilder.Entity<MenusInRole>(map => {
				map.HasKey(x => new { x.RoleId, x.SystemModuleId });
				map.HasOne(x => x.Role).WithMany(x => x.ModulesInRoles).HasForeignKey(x => x.RoleId); 
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
