using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KentNoteBook.Infrastructure.Html.TreeView
{
	public static class TreeViewHtmlHelperExtension
	{
		public static IHtmlContent TreeView(this IHtmlHelper htmlHelper, List<TreeViewNode> nodes, string editUrl = "", string removeUrl = "") {
			if ( nodes == null ) {
				throw new ArgumentNullException(nameof(nodes), "Please provide the treeview data.");
			}

			var htmlBuilder = new HtmlContentBuilder();
			var rootMenus = nodes.Where(x => x.ParentId == null).ToArray();

			htmlBuilder.AppendHtmlLine("<ul class='list-group list-group-flush'>");

			foreach ( var node in rootMenus ) {
				BuildMenuTree(node, nodes, htmlBuilder, editUrl, removeUrl);
			}

			htmlBuilder.AppendHtmlLine("</ul>");
			return htmlBuilder;
		}

		static void BuildMenuTree(TreeViewNode node, List<TreeViewNode> sources, HtmlContentBuilder htmlBuilder, string editUrl, string removeUrl) {
			var children = sources.Where(x => x.ParentId != null && node.Id != null && x.ParentId.ToString() == node.Id.ToString());

			htmlBuilder.AppendHtmlLine($"<li class='list-group-item {(children.Any() ? "" : "pl-4")} border-top'>");

			if ( children.Any() ) {
				htmlBuilder.AppendHtmlLine("<a class='collapse-icon' href='#'><i class='fa fa-fw fa-plus'></i></a>");
			}

			htmlBuilder.AppendHtmlLine("<div class='custom-control custom-checkbox d-inline'>");
			htmlBuilder.AppendHtmlLine($"<input type='checkbox' class='custom-control-input' id='node_{node.Id}' name='idArray' value='{node.Id}'>");
			htmlBuilder.AppendHtmlLine($"<label class='custom-control-label' for='node_{node.Id}'> {node.Name}</label>");
			htmlBuilder.AppendHtmlLine("</div>");

			// actions
			if ( editUrl != "" ) {
				htmlBuilder.AppendHtmlLine($"<a href='#' class='ml-1' data-toggle='modal' data-target='#modal_dialog_layout' data-modal-title='Edit' data-modal-url='{editUrl}/{node.Id}' data-modal-size='lg'><i class='fa fa-fw fa-edit'></i></a>");
			}
			if ( removeUrl != "" ) {
				htmlBuilder.AppendHtmlLine($"<a href='#' class='text-danger' data-toggle='modal' data-target='#modal_confirm_layout' data-url='{removeUrl}?handler=Remove&idArray={node.Id}' data-alert-panel='#alert_panel' data-update-panel='#menuContainer'><i class='fa fa-fw fa-remove'></i></a>");
			}

			if ( children.Any() ) {
				htmlBuilder.AppendHtmlLine($"<ul class='list-group list-group-flush d-none mt-1 pt-2' id='collapse_{node.Id}'>");

				foreach ( var child in children ) {
					BuildMenuTree(child, sources, htmlBuilder, editUrl, removeUrl);
				}

				htmlBuilder.AppendHtmlLine("</ul>");
			}

			htmlBuilder.AppendHtmlLine("</li>");
		}
	}
}
