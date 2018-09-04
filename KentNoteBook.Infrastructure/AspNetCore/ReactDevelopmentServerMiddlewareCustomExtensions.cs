using Microsoft.AspNetCore.Builder;
using System;

namespace Microsoft.AspNetCore.SpaServices.VueDevelopmentServer
{
	public static class VueDevelopmentServerMiddlewareCustomExtensions
	{
		public static void UseReactDevelopmentServer(
			   this ISpaBuilder spaBuilder,
			   string npmScript) {
			if ( spaBuilder == null ) {
				throw new ArgumentNullException(nameof(spaBuilder));
			}

			var spaOptions = spaBuilder.Options;

			if ( string.IsNullOrEmpty(spaOptions.SourcePath) ) {
				throw new InvalidOperationException($"To use {nameof(UseReactDevelopmentServer)}, you must supply a non-empty value for the {nameof(SpaOptions.SourcePath)} property of {nameof(SpaOptions)} when calling {nameof(SpaApplicationBuilderExtensions.UseSpa)}.");
			}

			VueDevelopmentServerMiddlewareCustom.Attach(spaBuilder, npmScript);
		}
	}
}
