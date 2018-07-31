using KentNoteBook.Infrastructure.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KentNoteBook.WebApp.Handlers
{
	[AllowAnonymous]
	public class AuthController : Controller
	{
		[Route("Auth/Test")]
		[HttpGet]
		public IActionResult Test() {
			return Ok("Success");
		}

		[Route("Auth/CheckToken")]
		[HttpPost]
		public IActionResult CheckToken() {
			if ( !this.User.Identity.IsAuthenticated ) {
				return new CustomResult(0, "Unauthorized");
			}
			return new SuccessResult();
		}
	}
}