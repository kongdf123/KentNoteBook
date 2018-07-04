using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KentNoteBook.Data;
using KentNoteBook.Infrastructure.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KentNoteBook.WebApp
{
	public class Startup
	{
		public Startup(ILoggerFactory loggerFactory, IConfiguration configuration, IHostingEnvironment env) {
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddJsonFile("appsettings.Deployment.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables()
				.AddUserSecrets<Startup>();

			Configuration = builder.Build();

			_logger = loggerFactory.CreateLogger("GlobalFiltersLogger");
		}

		ILogger _logger;
		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			// Setup options service
			services.AddOptions();

			services.AddMvc(options => { options.Filters.Add(new RazorPageFilter(_logger)); });
			services.AddDbContextPool<KentNoteBookDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("KentNoteBook")));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
			if ( env.IsDevelopment() ) {
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else {
				app.UseExceptionHandler("/Error");
			}

			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseMvc();
		}
	}
}
