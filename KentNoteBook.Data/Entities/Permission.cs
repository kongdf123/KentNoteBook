using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KentNoteBook.Data.Base;

namespace KentNoteBook.Data.Entities
{
    public class Permission : BaseEntity
	{
		[MaxLength(50), Required]
		public string Name { get; set; }


	}
}
