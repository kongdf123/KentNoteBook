using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KentNoteBook.Core.Base;

namespace KentNoteBook.Core.Entities
{
    public class SystemUserGroup : BaseEntity
	{
		[MaxLength(50), Required]
		public string Name { get; set; }

		[MaxLength(20), Required]
		public string Code { get; set; }

		public List<UsersInUserGroup> UsersInUserGroups { get; set; }
		public List<RolesInUserGroup> RolesInUserGroups { get; set; }
	}

	public class UsersInUserGroup
	{
		public Guid UserId { get; set; }
		public Guid UserGroupId { get; set; }

		public SystemUser User { get; set; }
		public SystemUserGroup UserGroup { get; set; }

	}
	public class RolesInUserGroup
	{
		public Guid RoleId { get; set; }
		public Guid UserGroupId { get; set; }

		public SystemRole Role { get; set; }
		public SystemUserGroup UserGroup { get; set; }
	}
}
