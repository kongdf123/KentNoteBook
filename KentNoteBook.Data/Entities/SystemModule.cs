using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KentNoteBook.Data.Base;

namespace KentNoteBook.Data.Entities
{
	public class SystemModule : BaseEntity
	{
		[Key]
		public override Guid Id { get; set; }

		[MaxLength(50), Required]
		public string Name { get; set; }

		public int Level { get; set; }

		public Status Status { get; set; }


		public List<SystemAction> SystemActions { get; set; } = new List<SystemAction>();
		public List<ModulesInRole> ModulesInRoles { get; set; } = new List<ModulesInRole>();
	}
}
