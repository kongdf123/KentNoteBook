using System;

namespace KentNoteBook.Data.Entities
{
	public class ModulesInRole
	{
		public Guid RoleId { get; set; }
		public Guid SystemModuleId { get; set; }

		public Role Role { get; set; }
		public SystemModule SystemModule { get; set; }
	}
}
