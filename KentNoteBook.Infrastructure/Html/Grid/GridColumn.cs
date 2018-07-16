using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KentNoteBook.Infrastructure.Html.Grid
{
	public class GridColumn
	{
		[JsonProperty("field")]
		public string Field { get; set; }

		[JsonProperty("type")]
		public GridColumnDataType DataType { get; set; }

		[JsonProperty("width")]
		public int Width { get; set; } = 20;

		[JsonProperty("hidden")]
		public bool Hidden { get; set; }

		[JsonProperty("format")]
		public string Format { get; set; }

		[JsonProperty("template")]
		public string Template { get; set; }
	}

	public enum GridColumnDataType
	{
		Object = 0,
		String = 1,
		Date = 2,
		Number = 3,
		Boolean = 4,
	}
}
