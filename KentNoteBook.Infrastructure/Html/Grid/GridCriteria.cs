using System;
using System.Collections.Generic;
using System.Text;

namespace KentNoteBook.Infrastructure.Html.Grid
{
	public class GridCriteria
	{
		public bool PaginationEnabled { get; set; } = true;
		public int Limit { get; set; } = 20;
		public int Offset { get; set; }

		public string SortBy { get; set; }
		public GridSortDirection SortDirection { get; set; }

		public List<GridColumnFilter> PostFilters { get; set; } = new List<GridColumnFilter>();
	}

	public class GridColumnFilter
	{
		public string Field { get; set; }
		public string Value { get; set; }
		public GridFilterOperator Operator { get; set; }
	}

	public enum GridSortDirection
	{
		Ascending = 0,
		Descending = 1,
	}

	public enum GridFilterOperator
	{
		Equal = 0,
		NotEqual = 1,
		Contains = 2,
		GreaterThan = 3,
		GreaterThanOrEqual = 4,
		LessThan = 5,
		LessThanOrEqual = 6,
	}
}
