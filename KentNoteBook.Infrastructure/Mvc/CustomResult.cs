using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace KentNoteBook.Infrastructure.Mvc
{
	public class CustomResult<T> : ActionResult
		where T : class
	{
		public CustomResult(int code, T data) {
			this.Code = code;
			this.Data = data;
		}

		/// <summary>
		/// To standard the result type,  for instance , 1:success, 0:failure ...
		/// </summary>
		public int Code { get; set; }

		public T Data { get; set; }
	}

	public class SuccessResult : CustomResult<string>
	{
		public SuccessResult() : base(1, "Successful") {
		}
	}
}
