using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KentNoteBook.Data;
using KentNoteBook.Infrastructure.Html.TreeView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KentNoteBook.WebApp.Pages.UserManagement.Partials
{
	public class MenuTreeModel : PageModel
	{
		public MenuTreeModel(KentNoteBookDbContext db) {
			this._db = db;
		}

		readonly KentNoteBookDbContext _db;

		public List<TreeViewNode> Data { get; set; } = new List<TreeViewNode>();

		public async Task<IActionResult> OnGetAsync() {

			this.Data = await _db.Menus
				.AsNoTracking()
				.Select(x => new TreeViewNode {
					Id = x.Id,
					ParentId = x.ParentId,

					Name = x.Name,
					ParentName = x.Parent.Name
				})
				.ToListAsync();

			return Page();
		}
	}
}