using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KentNoteBook.Infrastructure.Mvc
{
	public class CustomResult : ActionResult
	{
		public CustomResult(RequestResultCode code, object data) {
			this.Code = (int)code;
			this.Data = data;
		}

		public CustomResult(int code, object data) {
			this.Code = code;
			this.Data = data;
		}

		/// <summary>
		/// To standard the result type,  for instance , 1:success, 0:failure ...
		/// </summary>
		public int Code { get; set; }

		public object Data { get; set; }

		ActionResult Result {
			get {
				return new JsonResult(new { this.Code, this.Data });
			}
		}

		public override void ExecuteResult(ActionContext context) {
			Result.ExecuteResult(context);
		}

		public override async Task ExecuteResultAsync(ActionContext context) {
			await Result.ExecuteResultAsync(context);
		}
	}

	public class SuccessResult : CustomResult
	{
		public SuccessResult() : base(RequestResultCode.Success, "Success") {
		}
		public SuccessResult(int code, object data) : base((int)RequestResultCode.Success, data) {
		}
	}

	public class FailResult : CustomResult
	{
		public FailResult() : base(RequestResultCode.Failure, "Failure") {
		}
		public FailResult(int code, object data) : base((int)RequestResultCode.Failure, data) {
		}
	}

	public enum RequestResultCode
	{
		Success = 1,
		Failure = 0,
	}
}
