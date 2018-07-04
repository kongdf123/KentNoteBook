using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace KentNoteBook.Infrastructure.Mvc
{
	public class RazorPageFilter : IPageFilter
	{
		readonly ILogger _logger;

		public RazorPageFilter(ILogger logger) {
			_logger = logger;
		}

		public void OnPageHandlerSelected(PageHandlerSelectedContext context) {
			_logger.LogDebug("Global sync OnPageHandlerSelected called.");
		}

		public void OnPageHandlerExecuting(PageHandlerExecutingContext context) {
			_logger.LogDebug("Global sync PageHandlerExecutingContext called.");
		}

		public void OnPageHandlerExecuted(PageHandlerExecutedContext context) {
			_logger.LogDebug("Global sync OnPageHandlerExecuted called.");
		}
	}
}
