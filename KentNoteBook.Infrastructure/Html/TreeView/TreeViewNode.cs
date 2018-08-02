using System.Collections.Generic;

namespace KentNoteBook.Infrastructure.Html.TreeView
{
	public class TreeViewNode
	{
		public object Id { get; set; }
		public object ParentId { get; set; }

		public string Name { get; set; }
		public string ParentName { get; set; }

		public List<TreeViewNode> Children { get; set; } = new List<TreeViewNode>();
	}
}
