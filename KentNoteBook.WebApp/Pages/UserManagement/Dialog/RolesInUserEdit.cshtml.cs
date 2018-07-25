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
	public class RolesInUserEditModel : PageModel
	{
		public RolesInUserEditModel(KentNoteBookDbContext db) {
			this._db = db;
		}

		readonly KentNoteBookDbContext _db;

		[BindProperty(SupportsGet = true)]
		public Guid UserId { get; set; }

		[FromForm, BindProperty]
		public List<Guid> RoleInUserArray { get; set; } = new List<Guid>();

		public List<RoleModel> Roles { get; set; } = new List<RoleModel>();

		public class RoleModel
		{
			public Guid RoleId { get; set; }
			public string RoleName { get; set; }
		}

		public async Task<IActionResult> OnGetAsync() {

			var hasUser = await _db.Users
				.AsNoTracking()
				.Where(x => x.Id == this.UserId)
				.AnyAsync();
			if ( !hasUser ) {
				return new BadRequestResult();
			}

			this.RoleInUserArray = await _db.UsersInRoles
				.AsNoTracking()
				.Where(x => x.UserId == this.UserId)
				.Select(x => x.RoleId)
				.ToListAsync();

			this.Roles = await _db.Roles
				.AsNoTracking()
				.Where(x => x.Status == Status.Enabled)
				.Select(x => new RoleModel {
					RoleId = x.Id,
					RoleName = x.Name
				})
				.ToListAsync();

			return Page();
		}

		public async Task<IActionResult> OnPostSaveAsync() {

			var rolesInUser = await _db.UsersInRoles
				.Where(x => x.UserId == this.UserId)
				.ToListAsync();

			// Add the new added role for the current user
			if ( RoleInUserArray.Any() ) {
				
				foreach ( var item in RoleInUserArray ) {
					if ( !rolesInUser.Any(x => x.RoleId == item) ) {
						_db.Add(new UsersInRole {
							RoleId = item,
							UserId = this.UserId
						});
					}
				}
			}

			// Detach the removed role
			foreach ( var item in rolesInUser ) {
				if ( !RoleInUserArray.Contains(item.RoleId) ) {
					_db.Remove(item);
				}
			}

			await _db.SaveChangesAsync();

			return new SuccessResult();
		}
	}
}