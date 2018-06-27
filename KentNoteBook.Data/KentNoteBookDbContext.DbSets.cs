using KentNoteBook.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KentNoteBook.Data
{
	partial class KentNoteBookDbContext : DbContext
	{
		// System
		public DbSet<Role> Roles { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<MenusInRole> ModulesInRoles { get; set; }
		public DbSet<UsersInRole> UsersInRoles { get; set; }


	}
}
