using System.Collections.Generic;
using KentNoteBook.Infrastructure.Html.Grid;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace KentNoteBook.Service.Html
{
	public static class GridHtmlHelperExtension
	{
		public static IHtmlContent DataGrid(this IHtmlHelper htmlHelper, string name, string dataSourceUrl, List<GridColumn> columns, GridCriteria criteria = null) {

			criteria = criteria ?? new GridCriteria();

			var htmlBuilder = new HtmlContentBuilder();

			htmlBuilder.AppendHtml("<script>");
			htmlBuilder.AppendHtml("$(function(){");
			htmlBuilder.AppendHtml($"$('#{name}').dataGridBind('{dataSourceUrl}',{JsonConvert.SerializeObject(criteria)},{JsonConvert.SerializeObject(columns)});");
			htmlBuilder.AppendHtml("});");
			htmlBuilder.AppendHtml("</script>");

			return htmlBuilder;
		}
	}
}
