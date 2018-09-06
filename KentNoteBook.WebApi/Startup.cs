using System.Text;
using KentNoteBook.Core;
using KentNoteBook.Service.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace KentNoteBook.Service
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IHostingEnvironment env) {
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddJsonFile("appsettings.Deployment.json", optional: true, reloadOnChange: true)
				.AddUserSecrets<Startup>()
				.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {

			services.AddDbContextPool<KentNoteBookDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("KentNoteBook")));
			
			services.AddAuthentication(x => {
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x => {
				x.TokenValidationParameters = new TokenValidationParameters {
					NameClaimType = JwtClaimTypes.Name,
					RoleClaimType = JwtClaimTypes.Role,

					ValidIssuer = "http://localhost:10745",
					ValidAudience = "api",
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))
				};


			});

			services.AddMvc(options => {
				options.RespectBrowserAcceptHeader = true; // false by default

				options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
				options.OutputFormatters.RemoveType<TextOutputFormatter>();
				options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
			})
			.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if ( env.IsDevelopment() ) {
				app.UseDeveloperExceptionPage();
			}
			else {
				app.UseHsts();
			}

			app.UseMvc();

			app.UseAuthentication();
		}
	}
}
