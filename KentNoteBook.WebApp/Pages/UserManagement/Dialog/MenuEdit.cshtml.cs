using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KentNoteBook.Data;
using KentNoteBook.Data.Entities;
using KentNoteBook.Infrastructure.Html.TreeView;
using KentNoteBook.Infrastructure.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KentNoteBook.WebApp.Pages.UserManagement.Dialog
{
	public class MenuEditModel : PageModel
	{
		public MenuEditModel(KentNoteBookDbContext db) {
			this._db = db;
		}

		readonly KentNoteBookDbContext _db;

		[BindProperty(SupportsGet = true)]
		public Guid? Id { get; set; }

		[FromForm, BindProperty]
		public MenuModel Data { get; set; }

		public List<SelectListItem> Menus { get; set; } = new List<SelectListItem>();

		public class MenuModel
		{
			[Required]
			public string Name { get; set; }

			public Guid? ParentId { get; set; }
		}

		public async Task<IActionResult> OnGetAsync() {
			if ( !this.Id.HasValue ) {
				this.Data = new MenuModel();
				this.Id = Guid.NewGuid();
			}
			else {
				this.Data = await _db.Menus
					.AsNoTracking()
					.Where(x => x.Id == this.Id)
					.Select(x => new MenuModel {
						Name = x.Name,
						ParentId = x.ParentId
					})
					.SingleOrDefaultAsync();

				if ( this.Data == null ) {
					return new BadRequestResult();
				}
			}

			var sources = await _db.Menus
				.AsNoTracking()
				.Select(x => new TreeViewNode {
					Id = x.Id,
					ParentId = x.ParentId,
					Name = x.Name,
				})
				.ToListAsync();

			var rootMenus = sources.Where(x => x.ParentId == null).ToArray();
			var level = 0;

			foreach ( var item in rootMenus ) {
				BuildMenuTree(item, sources, level);
				level = 0;
			}

			this.Menus.Insert(0, new SelectListItem {
				Text = "Please select a item",
				Value = ""
			});

			return Page();
		}

		void BuildMenuTree(TreeViewNode node, List<TreeViewNode> sources, int level) {

			if ( node.ParentId == null ) {
				this.Menus.Add(new SelectListItem {
					Text = node.Name,
					Value = node.Id + "",
					Disabled = node.Id.ToString() == this.Id.ToString() || (node.ParentId ?? "").ToString() == this.Id.ToString()
				});
			}

			var children = sources.Where(x => x.ParentId != null && node.Id != null && x.ParentId.ToString() == node.Id.ToString()).ToList();

			if ( children.Any() ) {
				level++;

				var prefix = "&nbsp;";
				for ( int i = 0; i < level; i++ ) {
					prefix += "&nbsp;";
				}

				foreach ( var child in children ) {
					this.Menus.Add(new SelectListItem {
						Text = prefix + "├  " + child.Name,
						Value = child.Id + "",
						Disabled = child.Id.ToString() == this.Id.ToString() || (child.ParentId ?? "").ToString() == this.Id.ToString()
					});

					BuildMenuTree(child, sources, level);
				}
			}
		}

		public async Task<IActionResult> OnPostSaveAsync() {
			if ( !ModelState.IsValid ) {
				return ModelState.ToJsonResult();
			}

			var entity = await _db.Menus
				.Where(x => x.Id == this.Id)
				.SingleOrDefaultAsync();

			if ( entity == null ) {
				entity = new SystemMenu {
					Id = this.Id ?? Guid.NewGuid(),

					CreatedBy = this.User.Identity.Name,
					CreatedDate = DateTime.Now,
				};

				_db.Add(entity);
			}

			entity.Name = this.Data.Name;
			entity.ParentId = this.Data.ParentId;

			entity.UpdatedBy = this.User.Identity.Name;
			entity.UpdatedDate = DateTime.Now;

			await _db.SaveChangesAsync();

			return new SuccessResult();
		}
	}
}