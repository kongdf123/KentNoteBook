using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KentNoteBook.Data;
using KentNoteBook.Infrastructure.Authentication;
using KentNoteBook.Infrastructure.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KentNoteBook.WebApp
{
	public class Startup
	{
		public Startup(ILoggerFactory loggerFactory, IHostingEnvironment env) {
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddJsonFile("appsettings.Deployment.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables()
				.AddUserSecrets<Startup>();

			_env = env;
			_configuration = builder.Build();
			_logger = loggerFactory.CreateLogger("GlobalFiltersLogger");
		}

		IHostingEnvironment _env;
		ILogger _logger;
		IConfiguration _configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			// Setup options service
			services.AddOptions();

			services.AddAuthentication(x => {
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x => {

				if ( _env.IsDevelopment() ) {
					x.RequireHttpsMetadata = false;
				}

				x.SaveToken = true;

				x.TokenValidationParameters = new TokenValidationParameters {
					NameClaimType = JwtClaimTypes.Name,
					RoleClaimType = JwtClaimTypes.Role,

					ValidateIssuer = true,
					ValidIssuer = _configuration["JwtValidIssuer"],

					ValidateAudience = true,
					ValidAudience = _configuration["JwtValidAudience"],

					ValidateIssuerSigningKey = true,

					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]))
				};

				x.IncludeErrorDetails = true;
				x.Events = new JwtBearerEvents {
					OnAuthenticationFailed = (context) => {
						context.NoResult();

						context.Response.StatusCode = 401;
						context.Response.ContentType = "text/plain";

						return context.Response.WriteAsync(context.Exception.ToString());
					}
				};
			});

			services.AddMvc(options => { options.Filters.Add(new RazorPageFilter(_logger)); });
			services.AddDbContextPool<KentNoteBookDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("KentNoteBook")));
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

			app.UseAuthentication();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseMvc();
		}
	}
}
