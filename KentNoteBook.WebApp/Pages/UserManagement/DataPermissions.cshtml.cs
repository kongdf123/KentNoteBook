using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KentNoteBook.Data;
using KentNoteBook.Infrastructure.Html.Grid;
using KentNoteBook.Infrastructure.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KentNoteBook.WebApp.Pages.UserManagement
{
    public class DataPermissionsModel : PageModel
	{
		public DataPermissionsModel(KentNoteBookDbContext db) {
			this._db = db;
		}

		readonly KentNoteBookDbContext _db;

		[BindProperty(SupportsGet = true)]
		public Guid[] IdArray { get; set; }

		public void OnGet() {
		}

		public async Task<IActionResult> OnPostDataPermissionsAsync([FromForm] GridCriteria criteria) {
			return await _db.Permissions
				.AsNoTracking()
				.ToDataSourceJsonResultAsync(criteria);
		}

		public async Task<IActionResult> OnPostRemoveAsync() {

			if ( IdArray == null || IdArray.Length == 0 ) {
				return new CustomResult(0, "Please select one item to remove.");
			}

			var deletes = await _db.Permissions
				.Where(x => IdArray.Contains(x.Id))
				.ToListAsync();

			_db.RemoveRange(deletes);

			await _db.SaveChangesAsync();

			return new SuccessResult();
		}
	}
}