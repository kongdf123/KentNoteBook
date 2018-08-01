using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace KentNoteBook.Infrastructure.Html.Grid
{
	public static class GridHtmlHelperExtension
	{
		public static IHtmlContent DataGrid(this IHtmlHelper htmlHelper, string name, string dataSourceUrl, List<GridColumn> columns, GridCriteria criteria = null) {

			criteria = criteria ?? new GridCriteria();

			var htmlBuilder = new HtmlContentBuilder();

			htmlBuilder.AppendHtmlLine("<script>");
			htmlBuilder.AppendHtmlLine("$(function(){");
			htmlBuilder.AppendHtmlLine($"$('#{name}').dataGridBind('{dataSourceUrl}',{JsonConvert.SerializeObject(criteria)},{JsonConvert.SerializeObject(columns)});");
			htmlBuilder.AppendHtmlLine("});");
			htmlBuilder.AppendHtmlLine("</script>");

			return htmlBuilder;
		}
	}
}
