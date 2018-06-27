using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KentNoteBook.Service.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class HomeController : ControllerBase
	{
		[HttpGet("TestData")]
		public JsonResult Test() {
			return new JsonResult(new { Site = "Web API" });
		}

		[Produces("application/xml")]
		[HttpGet("Products")]
		public ActionResult<List<Product>> GetProducts() {
			return new List<Product> {
				new Product{ Id=Guid.NewGuid(),Name="Apple"},
				new Product{ Id=Guid.NewGuid(),Name="Huawei"}
			};
		}
	}
}