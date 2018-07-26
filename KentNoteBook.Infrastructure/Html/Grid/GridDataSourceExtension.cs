using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KentNoteBook.Infrastructure.Linq;

namespace KentNoteBook.Infrastructure.Html.Grid
{
	public static class GridDataSourceExtension
	{
		public static async Task<IActionResult> ToDataSourceJsonResultAsync<T>(this IQueryable<T> source, GridCriteria criteria) where T : class {

			if ( criteria.PostFilters != null && criteria.PostFilters.Count > 0 ) {
				foreach ( var item in criteria.PostFilters ) {
					source = source.Where(item.Field, item.Value, item.Operator);
				}
			}

			var count = await source.CountAsync();

			if ( !string.IsNullOrEmpty(criteria.SortBy) ) {
				source = source.OrderBy(criteria.SortBy, criteria.SortDirection);
			}

			List<T> items;

			if ( criteria.PaginationEnabled ) {
				items = await source.Skip(criteria.Offset).Take(criteria.Limit).ToListAsync();
			}
			else {
				items = await source.ToListAsync();
			}

			return new JsonResult(new { TotalCount = count, Data = items });
		}
	}
}
