using System;

namespace KentNoteBook.Data.Entities
{
	public class UsersInRole
	{
		public Guid UserId { get; set; }
		public Guid RoleId { get; set; }

		public User User { get; set; }
		public Role Role { get; set; }
	}
}
