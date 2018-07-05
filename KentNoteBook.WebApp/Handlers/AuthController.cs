using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KentNoteBook.WebApp.Handlers
{
	public class AuthController : Controller
	{
		[HttpPost]
		public IActionResult CreateToken() {
			// TODO
			return View();
		}

	}
}