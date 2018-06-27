using System;

namespace KentNoteBook.Data.Entities
{
	public class MenusInRole
	{
		public Guid RoleId { get; set; }
		public Guid SystemModuleId { get; set; }

		public Role Role { get; set; } 
	}
}
