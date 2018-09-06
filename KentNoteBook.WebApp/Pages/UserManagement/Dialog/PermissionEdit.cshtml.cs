using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KentNoteBook.Core;
using KentNoteBook.Core.Entities;
using KentNoteBook.Core.Enums;
using KentNoteBook.Infrastructure.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KentNoteBook.WebApp.Pages.UserManagement.Dialog
{
	public class PermissionEditModel : PageModel
	{
		public PermissionEditModel(KentNoteBookDbContext db) {
			this._db = db;
		}

		readonly KentNoteBookDbContext _db;

		[BindProperty(SupportsGet = true)]
		public Guid? Id { get; set; }

		[BindProperty(SupportsGet = true)]
		public PermissionType PermissionType { get; set; }

		[FromForm, BindProperty]
		public PermissionModel Data { get; set; }

		public List<SelectListItem> PermissionTypeItems { get; set; } = new List<SelectListItem>();

		public class PermissionModel
		{
			[Required]
			public string Name { get; set; }

			[Required]
			public string Code { get; set; }

			public PermissionType PermissionType { get; set; }
		}

		public async Task<IActionResult> OnGetAsync() {
			if ( !this.Id.HasValue ) {
				this.Data = new PermissionModel {
					PermissionType = this.PermissionType
				};

				this.Id = Guid.NewGuid();
			}
			else {
				this.Data = await _db.Permissions
					.AsNoTracking()
					.Where(x => x.Id == this.Id)
					.Select(x => new PermissionModel {
						Name = x.Name,
						Code = x.Code,
						PermissionType = x.PermissionType,
					})
					.SingleOrDefaultAsync();

				if ( this.Data == null ) {
					return new BadRequestResult();
				}
			}

			this.PermissionTypeItems = Enum.GetValues(typeof(PermissionType))
				.Cast<PermissionType>()
				.Select(x => new SelectListItem {
					Text = x.ToString(),
					Value = x.ToString()
				})
				.ToList();

			this.PermissionTypeItems.Insert(0, new SelectListItem {
				Text = "Please select a item",
				Value = ""
			});

			return Page();
		}

		public async Task<IActionResult> OnPostSaveAsync() {
			if ( !ModelState.IsValid ) {
				return ModelState.ToJsonResult();
			}

			var entity = await _db.Permissions
				.Where(x => x.Id == this.Id)
				.SingleOrDefaultAsync();

			if ( entity == null ) {
				entity = new SystemPermission {
					Id = this.Id ?? Guid.NewGuid(),

					PermissionType = this.Data.PermissionType,

					CreatedBy = this.User.Identity.Name,
					CreatedDate = DateTime.Now,
				};

				_db.Add(entity);
			}

			entity.Name = this.Data.Name;
			entity.Code = this.Data.Code;

			entity.UpdatedBy = this.User.Identity.Name;
			entity.UpdatedDate = DateTime.Now;

			await _db.SaveChangesAsync();

			return new SuccessResult();
		}
	}
}