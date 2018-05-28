using KentNoteBook.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KentNoteBook.Data
{
	partial class KentNoteBookDbContext : DbContext
	{
		// System
		public DbSet<Role> Roles { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<SystemAction> SystemActions { get; set; }
		public DbSet<SystemModule> SystemModules { get; set; }
		public DbSet<ModulesInRole> ModulesInRoles { get; set; }
		public DbSet<UsersInRole> UsersInRoles { get; set; }


	}
}
