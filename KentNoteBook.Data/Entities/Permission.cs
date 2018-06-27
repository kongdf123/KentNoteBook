using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KentNoteBook.Data.Base;
using KentNoteBook.Data.Enums;

namespace KentNoteBook.Data.Entities
{
	public class Permission : BaseEntity
	{
		[MaxLength(50), Required]
		public string Name { get; set; }

		public PermissionType PermissionType { get; set; }

		public List<OperationsInPermission> OperationsInPermissions { get; set; }
	}

	public class OperationsInPermission
	{
		public Guid PermissionId { get; set; }
		public Guid OperationId { get; set; }

		public Permission Permission { get; set; }
		public Operation Operation { get; set; }
	}
}
