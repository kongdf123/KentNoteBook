using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace KentNoteBook.Service.Html
{
	public static class GridHtmlHelper
	{
		public static IHtmlString DataGrid<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> fieldExpression, IDictionary<string, object> htmlAttributes) where TProperty : class {

			// get the metdata
			ModelMetadata fieldmetadata = htmlHelper.ViewData.ModelMetadata;

			// get the field name
			var fieldName = ExpressionHelper.GetExpressionText(fieldExpression);
			
			// get full field (template may have prefix)
			var fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);

			// get value
			var value = fieldmetadata..Model.ToString();

			// build simple textbox html
			var tag = new Microsoft.AspNetCore.Mvc.Rendering.TagBuilder("input");
			tag.Attributes.Add("type", "text");
			tag.Attributes.Add("name", fullName);
			tag.Attributes.Add("value", value);
			//tag.GenerateId(fullName);  //replace ".[]()" with "_"

			// add passed html attributes
			tag.MergeAttributes(htmlAttributes);

			// add validation attributes
			if ( !string.IsNullOrEmpty(fieldName) )
				tag.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(fieldName));

			var html = tag.ToString();

			// return html
			return MvcHtmlString.Create(tag.ToString());

		}
	}
}
