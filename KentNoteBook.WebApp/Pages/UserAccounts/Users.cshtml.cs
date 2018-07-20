using System;
using System.Linq;
using System.Threading.Tasks;
using KentNoteBook.Data;
using KentNoteBook.Infrastructure.Cache;
using KentNoteBook.Infrastructure.Html.Grid;
using KentNoteBook.Infrastructure.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace KentNoteBook.WebApp.Pages.UserAccounts
{
	[AllowAnonymous]
	public class UsersModel : PageModel
	{
		public UsersModel(KentNoteBookDbContext db, IDistributedCache cache) {
			this._db = db;
			this._cache = cache;
		}

		readonly KentNoteBookDbContext _db;
		readonly IDistributedCache _cache;

		[BindProperty(SupportsGet = true)]
		public Guid[] IdArray { get; set; }

		public class UsersQueryCriterias
		{
			public string Key { get; set; }
			public string Value { get; set; }
		}

		public void OnGet() {

			var criterias = new UsersQueryCriterias { Key = "test", Value = "teest" };

			_cache.SetCache("UsersQueryCriterias", criterias);

		}

		public async Task<IActionResult> OnPostUsersAsync([FromForm] GridCriteria criteria) {
			return await _db.Users.ToDataSourceJsonResultAsync(criteria);
		}

		public async Task<IActionResult> OnPostRemoveAsync() {

			if ( IdArray == null || IdArray.Length == 0 ) {
				return new RequestResult(0, "Please select one item to remove.");
			}

			var deletes = await _db.Users
				.Where(x => IdArray.Contains(x.Id))
				.ToListAsync();

			_db.RemoveRange(deletes);

			await _db.SaveChangesAsync();

			return new SuccessResult();
		}
	}
}