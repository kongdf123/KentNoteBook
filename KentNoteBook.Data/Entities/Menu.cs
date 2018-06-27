using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KentNoteBook.Data.Base;

namespace KentNoteBook.Data.Entities
{
    public class Menu : BaseEntity
	{
		[MaxLength(50), Required]
		public string Name { get; set; }
		
		public int Level { get; set; }

		public Status Status { get; set; }

		public List<PermissionsInMenu> PermissionsInMenus { get; set; }
	}

	public class PermissionsInMenu
	{
		public Guid MenuId { get; set; }
		public Guid PermissionId { get; set; }

		public Menu Menu { get; set; }
		public Permission Permission { get; set; }
	}
}
