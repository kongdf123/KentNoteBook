using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KentNoteBook.Core;
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

		[BindProperty(SupportsGet = true)]
		public bool ShowCheckBox { get; set; }

		[BindProperty(SupportsGet = true)]
		public bool ShowEditButton { get; set; }

		[BindProperty(SupportsGet = true)]
		public bool ShowRemoveButton { get; set; }

		[BindProperty(SupportsGet = true)]
		public bool ShowPermissionsTable { get; set; }

		public List<TreeViewNode> Data { get; set; } = new List<TreeViewNode>();

		public async Task<IActionResult> OnGetAsync() {

			this.Data = await _db.Menus
				.AsNoTracking()
				.Select(x => new TreeViewNode {
					Id = x.Id,
					ParentId = x.ParentId,

					Name = x.Name,
					ParentName = x.Parent.Name,

					Permissions = !this.ShowPermissionsTable ? null : x.PermissionsInMenus.Select(y => y.Permission).ToList()
				})
				.ToListAsync();

			if ( this.Data.Any() ) {
				var menuIdCollection = this.Data.Select(x => (Guid)x.Id).ToList();
				var permissionsInMenus = await _db.PermissionsInMenus
					.AsNoTracking()
					.Where(x => menuIdCollection.Contains(x.MenuId))
					.Select(x => x.Permission)
					.ToListAsync();

				this.Data.ForEach(x=> {
					x.Permissions = permissionsInMenus.Where(y => y.Id == (Guid)x.Id).ToList();
				});
			}

			return Page();
		}
	}
}