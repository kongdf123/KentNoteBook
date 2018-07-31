using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KentNoteBook.Data;
using KentNoteBook.Data.Entities;
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

		public List<MenuModel> Data { get; set; } = new List<MenuModel>();

		public class MenuModel
		{
			public Guid Id { get; set; }
			public Guid? ParentId { get; set; }

			public string Name { get; set; }
			public string ParentName { get; set; }

			public List<MenuModel> Children { get; set; } = new List<MenuModel>();
		}

		public async Task<IActionResult> OnGetAsync() {

			var menus = await _db.Menus
				.AsNoTracking()
				.Select(x => new MenuModel {
					Id = x.Id,
					ParentId = x.ParentId,

					Name = x.Name,
					ParentName = x.Parent.Name
				})
				.ToListAsync();

			Data.AddRange(menus.Where(x => x.ParentId == null).ToList());

			return Page();
		}

		void BuildMenuTree(SystemMenu menu) {
			var menuTree = new List<MenuModel>();
			 


		}
	}
}