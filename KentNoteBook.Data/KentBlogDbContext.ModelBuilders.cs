using KentNoteBook.Data.Entities;
using Microsoft.EntityFrameworkCore;

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

			modelBuilder.Entity<ModulesInRole>(map => {
				map.HasKey(x => new { x.RoleId, x.SystemModuleId });
				map.HasOne(x => x.Role).WithMany(x => x.ModulesInRoles).HasForeignKey(x => x.RoleId);
				map.HasOne(x => x.SystemModule).WithMany(x => x.ModulesInRoles).HasForeignKey(x => x.SystemModuleId);
			});


		}
	}
}
