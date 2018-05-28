using Microsoft.EntityFrameworkCore;

namespace KentNoteBook.Data
{
	public partial class KentNoteBookDbContext : DbContext
	{
		public KentNoteBookDbContext(DbContextOptions<KentNoteBookDbContext> options) : base(options) { }

	}
}
