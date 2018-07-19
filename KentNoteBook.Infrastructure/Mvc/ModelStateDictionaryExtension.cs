using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KentNoteBook.Infrastructure.Mvc
{
	public static class ModelStateDictionaryExtension
	{
		public static JsonResult ToJsonResult(this ModelStateDictionary modelState) {
			if ( !modelState.IsValid ) {

				var errors = modelState.Values
					.SelectMany(m => m.Errors)
					.Select(e => e.ErrorMessage)
					.ToList();

				return new JsonResult(new { Code = 0, Data = errors });
			}

			return new JsonResult(new { Code = 1, Data = "Successful" });
		}
	}
}
