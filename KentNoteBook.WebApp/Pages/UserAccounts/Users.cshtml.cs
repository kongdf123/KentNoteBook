using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KentNoteBook.Infrastructure.Cache;
using KentNoteBook.Infrastructure.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;

namespace KentNoteBook.WebApp.Pages.UserAccounts
{
	[AllowAnonymous]
	public class UsersModel : PageModel
	{
		public UsersModel(IDistributedCache cache) {
			this._cache = cache;
		}

		readonly IDistributedCache _cache;

		public class UsersQueryCriterias
		{
			public string Key { get; set; }
			public string Value { get; set; }
		}

		public void OnGet() {
			var criterias = new UsersQueryCriterias { Key = "test", Value = "teest" };

			_cache.SetCache("UsersQueryCriterias", criterias);

		}
	}
}