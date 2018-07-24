using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KentNoteBook.Data;
using KentNoteBook.Data.Entities;
using KentNoteBook.Infrastructure.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KentNoteBook.WebApp.Pages.UserManagement.Dialog
{
	[AllowAnonymous]
	public class RoleEditModel : PageModel
	{
		public RoleEditModel(KentNoteBookDbContext db) {
			this._db = db;
		}

		readonly KentNoteBookDbContext _db;

		[BindProperty(SupportsGet = true)]
		public Guid? Id { get; set; }

		[FromForm, BindProperty]
		public RoleModel Data { get; set; }

		public class RoleModel
		{
			[Required]
			public string Name { get; set; }

			[StringLength(500)]
			public string Description { get; set; }

			public bool IsActive { get; set; }
		}

		public async Task<IActionResult> OnGetAsync() {

			if ( !this.Id.HasValue ) {
				this.Data = new RoleModel();
				this.Id = Guid.NewGuid();
			}
			else {
				this.Data = await _db.Roles
					.AsNoTracking()
					.Where(x => x.Id == this.Id)
					.Select(x => new RoleModel {
						Name = x.Name,
						Description = x.Description,
						IsActive = x.IsActive,
					})
					.SingleOrDefaultAsync();

				if ( this.Data == null ) {
					return new BadRequestResult();
				}

			}

			return Page();
		}

		public async Task<IActionResult> OnPostSaveAsync() {

			System.Threading.Thread.Sleep(2000);

			if ( !ModelState.IsValid ) {
				return ModelState.ToJsonResult();
			}

			var entity = await _db.Roles
				.Where(x => x.Id == this.Id)
				.SingleOrDefaultAsync();

			if ( entity == null ) {

				entity = new SystemRole {
					Id = this.Id ?? Guid.NewGuid(),

					Status = Status.Enabled,
					CreatedBy = "Admin",
					CreatedDate = DateTime.Now,
				};

				_db.Roles.Add(entity);
			}

			entity.Name = this.Data.Name;
			entity.IsActive = this.Data.IsActive;
			entity.Description = this.Data.Description;

			entity.UpdatedBy = "Admin";
			entity.UpdatedDate = DateTime.Now;

			await _db.SaveChangesAsync();

			return new SuccessResult();
		}
	}
}