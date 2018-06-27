using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KentNoteBook.Data.Base;

namespace KentNoteBook.Data.Entities
{
    public class UserGroup : BaseEntity
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

		public User User { get; set; }
		public UserGroup UserGroup { get; set; }

	}
	public class RolesInUserGroup
	{
		public Guid RoleId { get; set; }
		public Guid UserGroupId { get; set; }

		public Role Role { get; set; }
		public UserGroup UserGroup { get; set; }
	}
}
