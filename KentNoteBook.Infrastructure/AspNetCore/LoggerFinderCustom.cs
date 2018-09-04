using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Microsoft.AspNetCore.SpaServices.Util
{
	public static class LoggerFinderCustom
	{
		public static ILogger GetOrCreateLogger(
			   IApplicationBuilder appBuilder,
			   string logCategoryName) {
			// If the DI system gives us a logger, use it. Otherwise, set up a default one.
			var loggerFactory = appBuilder.ApplicationServices.GetService<ILoggerFactory>();
			var logger = loggerFactory != null
				? loggerFactory.CreateLogger(logCategoryName)
				: new ConsoleLogger(logCategoryName, null, false);
			return logger;
		}
	}
}
