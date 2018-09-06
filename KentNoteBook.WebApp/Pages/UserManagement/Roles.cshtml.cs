using System;
using System.Linq;
using System.Threading.Tasks;
using KentNoteBook.Core;
using KentNoteBook.Infrastructure.Html.Grid;
using KentNoteBook.Infrastructure.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KentNoteBook.WebApp.Pages.UserAccounts
{
	public class SystemRolesModel : PageModel
	{
		public SystemRolesModel(KentNoteBookDbContext db) {
			this._db = db;
		}

		readonly KentNoteBookDbContext _db;

		[BindProperty(SupportsGet = true)]
		public Guid[] IdArray { get; set; }

		public void OnGet() {
		}

		public async Task<IActionResult> OnPostRolesAsync([FromForm] GridCriteria criteria) {
			return await _db.Roles
				.AsNoTracking()
				.ToDataSourceJsonResultAsync(criteria);
		}

		public async Task<IActionResult> OnPostRemoveAsync() {

			if ( IdArray == null || IdArray.Length == 0 ) {
				return new CustomResult(0, "Please select one item to remove.");
			}

			var deletes = await _db.Roles
				.Where(x => IdArray.Contains(x.Id))
				.ToListAsync();

			_db.RemoveRange(deletes);

			await _db.SaveChangesAsync();

			return new SuccessResult();
		}
	}
}