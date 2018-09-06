using Microsoft.EntityFrameworkCore;

namespace KentNoteBook.Core
{
	public partial class KentNoteBookDbContext : DbContext
	{
		public KentNoteBookDbContext(DbContextOptions<KentNoteBookDbContext> options) : base(options) { }

	}
}
