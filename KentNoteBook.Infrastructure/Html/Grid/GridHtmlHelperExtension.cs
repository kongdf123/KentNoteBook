using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using KentNoteBook.Infrastructure.Html.Grid;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Newtonsoft.Json;

namespace KentNoteBook.Service.Html
{
	public static class GridHtmlHelperExtension
	{
		public static IHtmlContent DataGrid(this IHtmlHelper htmlHelper, string name, string dataSourceUrl, List<GridColumn> columns, GridCriteria criteria = null) {

			criteria = criteria ?? new GridCriteria();
			//?? typeof(IViewModel).GetProperties(BindingFlags.Instance | BindingFlags.Public)
			//.Select(x => new GridColumn {
			//	Field = x.Name,
			//	DataType = GridColumnDataType.String
			//})
			//.ToList();

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
