using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KentNoteBook.Service.Controllers
{
	[Produces("application/json")]
	[Route("api/[controller]")]
	[ApiController]
	public class HomeController : ControllerBase
	{
		[HttpGet]
		public JsonResult Test() {
			return new JsonResult(new { Site = "Web API" });
		}
	}
}