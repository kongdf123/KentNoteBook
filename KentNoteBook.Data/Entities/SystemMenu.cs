using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KentNoteBook.Data.Base;

namespace KentNoteBook.Data.Entities
{
    public class SystemMenu : BaseEntity
	{
		[MaxLength(50), Required]
		public string Name { get; set; }
		
		public Guid? ParentId { get; set; }

		public List<PermissionsInMenu> PermissionsInMenus { get; set; }
	}

	public class PermissionsInMenu
	{
		public Guid MenuId { get; set; }
		public Guid PermissionId { get; set; }

		public SystemMenu Menu { get; set; }
		public SystemPermission Permission { get; set; }
	}
}
