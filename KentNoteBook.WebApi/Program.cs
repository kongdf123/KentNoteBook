using KentNoteBook.Core;
using KentNoteBook.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KentNoteBook.Service
{
	public class Program
	{
		public static void Main(string[] args) {
			var host = BuildWebHost(args);

			using ( var serviceScope = host.Services.GetService<IServiceScopeFactory>().CreateScope() ) {
				var context = serviceScope.ServiceProvider.GetRequiredService<KentNoteBookDbContext>();
				context.Database.Migrate();

				context.SeedData();
			}

			host.Run();
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.Build();
	}
}
