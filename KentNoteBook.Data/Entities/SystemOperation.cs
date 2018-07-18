using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KentNoteBook.Data.Base;

namespace KentNoteBook.Data.Entities
{
    public class SystemOperation : BaseEntity
	{
		[MaxLength(50), Required]
		public string Name { get; set; }

		[MaxLength(20), Required]
		public string Code { get; set; }

	}
}
