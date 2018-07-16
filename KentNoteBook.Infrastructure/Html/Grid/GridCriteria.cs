using System;
using System.Collections.Generic;
using System.Text;
using KentNoteBook.Infrastructure.Linq;

namespace KentNoteBook.Infrastructure.Html.Grid
{
	public class GridCriteria
	{
		public bool PaginationEnabled { get; set; } = true;
		public int Limit { get; set; } = 20;
		public int Offset { get; set; }

		public string SortBy { get; set; }
		public SortDirection SortDirection { get; set; }

		public List<GridColumnFilter> PostFilters { get; set; } = new List<GridColumnFilter>();
	}

	public class GridColumnFilter
	{
		public string Field { get; set; }
		public string Value { get; set; }
		public FilterOperator Operator { get; set; }
	}
}
