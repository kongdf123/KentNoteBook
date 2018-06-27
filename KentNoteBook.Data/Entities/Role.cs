using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KentNoteBook.Data.Base;

namespace KentNoteBook.Data.Entities
{
	public class Role : BaseEntity
	{
		[Key]
		public override Guid Id { get; set; }

		[MaxLength(50), Required]
		public string Name { get; set; }

		public Status Status { get; set; }


		public List<UsersInRole> UsersInRoles { get; set; } = new List<UsersInRole>();
		public List<MenusInRole> ModulesInRoles { get; set; } = new List<MenusInRole>();
	}
}
