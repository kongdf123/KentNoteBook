using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KentNoteBook.Infrastructure.Mvc
{
	public class CustomResult<T> : ActionResult
		where T : class
	{
		public CustomResult() { }

		public CustomResult(CustomResultCode code, T data) {
			this.Code = code;
			this.Data = data;
		}

		/// <summary>
		/// To standard the result type,  for instance , 1:success, 0:failure ...
		/// </summary>
		public CustomResultCode Code { get; set; }

		public T Data { get; set; }

		public string ToJson() {
			return JsonConvert.SerializeObject(this);
		}
	}

	public class SuccessResult : CustomResult<string>
	{
		public SuccessResult() : base(CustomResultCode.Success, "Successful") {
		}
	}

	public enum CustomResultCode
	{
		Success = 1,
		Failure = 0,
	}
}
