using System;
using System.ComponentModel.DataAnnotations;
using KentNoteBook.Data.Base;

namespace KentNoteBook.Data.Entities
{
	public class SystemAction : BaseEntity
	{
		[Key]
		public override Guid Id { get; set; }

		public Guid SystemModuleId { get; set; }

		[MaxLength(50), Required]
		public string Name { get; set; }

		[MaxLength(30), Required]
		public string Code { get; set; }


		public SystemModule SystemModule { get; set; }
	}
}
