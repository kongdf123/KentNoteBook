using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KentNoteBook.Core.Base;

namespace KentNoteBook.Core.Entities
{
	public class SystemUser : BaseEntity
	{
		[Key]
		public override Guid Id { get; set; }

		[MaxLength(50), Required]
		public string Name { get; set; }

		[MaxLength(30)]
		public string NickName { get; set; }

		[MaxLength(50), Required]
		public string Password { get; set; }

		[MaxLength(30)]
		public string PasswordSalt { get; set; }

		[MaxLength(100), Required]
		public string Email { get; set; }

		[MaxLength(30)]
		public string Mobile { get; set; }

		[MaxLength(150)]
		public string Avatar { get; set; }

		[MaxLength(500)]
		public string Discription { get; set; }

		public bool IsActive { get; set; }

		public Status Status { get; set; }
	}
}
