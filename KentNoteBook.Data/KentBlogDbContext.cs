using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace KentNoteBook.Data
{
	public partial class KentBlogDbContext : DbContext
	{
		public KentBlogDbContext(DbContextOptions<KentBlogDbContext> options) : base(options) { }

	}
}
