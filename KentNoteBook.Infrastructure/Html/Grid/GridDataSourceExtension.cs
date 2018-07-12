using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KentNoteBook.Infrastructure.Html.Grid
{
	public static class GridDataSourceExtension
	{
		public static async Task<IActionResult> ToDataSourceJsonResultAsync<T>(this IQueryable<T> query, GridCriteria criteria) {

			// TODO : apply the criterias from the client

			return new JsonResult(new { TotalCount = await query.CountAsync(), Data = await query.ToListAsync() });
		}
	}
}
