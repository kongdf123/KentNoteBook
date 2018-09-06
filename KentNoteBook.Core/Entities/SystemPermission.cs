using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KentNoteBook.Core.Base;
using KentNoteBook.Core.Enums;

namespace KentNoteBook.Core.Entities
{
	public class SystemPermission : BaseEntity
	{
		[MaxLength(50), Required]
		public string Name { get; set; }

		[MaxLength(20), Required]
		public string Code { get; set; }

		public PermissionType PermissionType { get; set; }

		public List<OperationsInPermission> OperationsInPermissions { get; set; }
	}

	public class OperationsInPermission
	{
		public Guid PermissionId { get; set; }
		public Guid OperationId { get; set; }

		public SystemPermission Permission { get; set; }
		public SystemOperation Operation { get; set; }
	}
}
