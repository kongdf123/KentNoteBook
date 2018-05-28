using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KentNoteBook.Data.Entities
{
	public class User
	{
		[Key]
		public Guid Id { get; set; }

		[MaxLength(50)]
		public string Name { get; set; }

		[MaxLength(30)]
		public string NickName { get; set; }

		[MaxLength(100)]
		public string Email { get; set; }

		[MaxLength(150)]
		public string Avatar { get; set; }

		[MaxLength(500)]
		public string Discription { get; set; }
	}
}
