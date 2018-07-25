using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KentNoteBook.Infrastructure.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace KentNoteBook.WebApp.Handlers
{
	public class AuthController : Controller
	{
		[HttpPost]
		public IActionResult CheckToken() {
			if ( !this.User.Identity.IsAuthenticated ) {
				return new CustomResult(0, "Unauthorized");
			}
			return new SuccessResult();
		}
	}
}