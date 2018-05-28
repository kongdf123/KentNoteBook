using System;
using System.ComponentModel.DataAnnotations;

namespace KentNoteBook.Data.Base
{
	public abstract class BaseEntity
	{
		[Key]
		public virtual Guid Id { get; set; }

		public virtual DateTime CreatedDate { get; set; }
		public virtual string CreatedBy { get; set; }

		public virtual DateTime UpdatedDate { get; set; }
		public virtual string UpdatedBy { get; set; }
	}
}
