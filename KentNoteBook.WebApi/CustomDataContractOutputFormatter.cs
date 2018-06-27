using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace KentNoteBook.Service
{
	public class CustomDataContractOutputFormatter : XmlSerializerOutputFormatter
	{
		protected override bool CanWriteType(Type type) {
			if ( typeof(Product).IsAssignableFrom(type)
				|| typeof(IEnumerable<Product>).IsAssignableFrom(type) ) {
				return base.CanWriteType(type);
			}
			return false;
		}

		public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding) {
			IServiceProvider serviceProvider = context.HttpContext.RequestServices;
			var logger = serviceProvider.GetService(typeof(ILogger<CustomDataContractOutputFormatter>)) as ILogger;

			var response = context.HttpContext.Response;

			var buffer = new StringBuilder();
			if ( context.Object is IEnumerable<Product> ) {
				foreach ( Product contact in context.Object as IEnumerable<Product> ) {
					FormatVcard(buffer, contact, logger);
				}
			}
			else {
				var contact = context.Object as Product;
				FormatVcard(buffer, contact, logger);
			}
			return response.WriteAsync(buffer.ToString());
		}

		private static void FormatVcard(StringBuilder buffer, Product product, ILogger logger) {

			buffer.Append(Serialize(product));

			logger.LogInformation($"Writing {product.Id} {product.Name}");
		}

		static string Serialize<T>(T value) {
			if ( value == null ) {
				return string.Empty;
			}
			try {
				var xmlserializer = new XmlSerializer(typeof(T));
				var stringWriter = new StringWriter();
				using ( var writer = XmlWriter.Create(stringWriter) ) {
					xmlserializer.Serialize(writer, value);
					return stringWriter.ToString();
				}
			}
			catch ( Exception ex ) {
				throw new Exception("An error occurred", ex);
			}
		}
	}
}
