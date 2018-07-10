using System;
using System.Net;
using System.Text;
using KentNoteBook.Data;
using KentNoteBook.Infrastructure.Authentication;
using KentNoteBook.Infrastructure.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
				.AddEnvironmentVariables();

			if ( env.IsDevelopment() ) {
				builder.AddUserSecrets<Startup>();
			}

			this._env = env;
			this._configuration = builder.Build();
			this._logger = loggerFactory.CreateLogger("GlobalFiltersLogger");
		}

		IHostingEnvironment _env;
		ILogger _logger;
		IConfiguration _configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			// Setup options service
			services.AddOptions();

			//ConfigureJwtAuthService(services);

			ConfigureDistributedCacheService(services);

			services.AddMvc(options => {
				options.Filters.Add(new RazorPageFilter(_logger));

				//var policy = new AuthorizationPolicyBuilder()
				//	.RequireAuthenticatedUser()
				//	.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
				//	.Build();
				//options.Filters.Add(new AuthorizeFilter(policy));
			})
			.AddRazorPagesOptions(options => {
				options.Conventions.AuthorizeFolder("/"); // Require users to be authenticated.
														  //options.Conventions.AuthorizeFolder("/", "YourPolicyName"); // Require a policy to be full filled globally.
			})
			.AddJsonOptions(options => {
				options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				options.SerializerSettings.ContractResolver = new DefaultContractResolver();
				options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
			});

			services.AddDbContextPool<KentNoteBookDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("KentNoteBook")));
		}

		void ConfigureJwtAuthService(IServiceCollection services) {

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

					// Validate the token expiry  
					ValidateLifetime = true,

					// The signing key must match! 
					ValidateIssuerSigningKey = true,

					ClockSkew = TimeSpan.Zero,

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

		}

		void ConfigureDistributedCacheService(IServiceCollection services) {
			services.AddDistributedSqlServerCache(options => {
				options.ConnectionString = _configuration.GetConnectionString("KentNoteBookCache");
				options.SchemaName = "dbo";
				options.TableName = "SiteDataCache";
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
			//app.UseStatusCodePages("text/plain", "The server returned HTTP {0} status code.");

			app.UseStatusCodePages(async context => {
				var response = context.HttpContext.Response;

				if ( response.StatusCode == (int)HttpStatusCode.Unauthorized ||
				response.StatusCode == (int)HttpStatusCode.Forbidden ) {
					var result = new CustomResult<string> {
						Code = CustomResultCode.Failure,
						Data = $"The server returned HTTP {response.StatusCode} status code."
					};

					await response.WriteAsync(result.ToJson());
				}
			});

			app.UseAuthentication();

			if ( env.IsDevelopment() ) {
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else {
				app.UseExceptionHandler("/Error");
			}

			app.UseStaticFiles();
			app.UseMvc();

		}
	}
}
