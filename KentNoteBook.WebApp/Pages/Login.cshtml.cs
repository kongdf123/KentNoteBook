using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KentNoteBook.Data;
using KentNoteBook.Infrastructure.Authentication;
using KentNoteBook.Infrastructure.Cache;
using KentNoteBook.Infrastructure.Mvc;
using KentNoteBook.Infrastructure.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace KentNoteBook.WebApp.Pages
{
	[AllowAnonymous]
	public class LoginModel : PageModel
	{
		public LoginModel(KentNoteBookDbContext db, IConfiguration configuration, IDistributedCache cache) {
			this._db = db;
			this._configuration = configuration;
			this._cache = cache;
		}

		readonly KentNoteBookDbContext _db;
		readonly IConfiguration _configuration;
		readonly IDistributedCache _cache;

		[FromForm, FromQuery, BindProperty]
		public UserModel Data { get; set; }

		[BindProperty(SupportsGet = true)]
		public string JwtToken { get; set; }

		public class UserModel
		{
			[Required]
			[StringLength(50)]
			public string UserName { get; set; }

			[Required]
			[StringLength(30)]
			public string Password { get; set; }
		}

		public IActionResult OnGet() {

			this.Data = new UserModel();

			_cache.SetCache("UserModel", Data);

			return Page();
		}

		public async Task<IActionResult> OnPostSubmitAsync() {
			if ( !ModelState.IsValid ) {
				return ModelState.ToJsonResult();
			}

			var user = await _db.Users
				.AsNoTracking()
				.Where(x => x.Name == Data.UserName)
				.Where(x => x.Password == Data.Password)
				.Where(x => x.IsActive)
				.Where(x => x.Status == Status.Enabled)
				.SingleOrDefaultAsync();
			if ( user == null ) {
				return new CustomResult(0, "No such user.");
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["JwtSecurityKey"]);

			var authTime = DateTime.UtcNow;

			var tokenDescriptor = new SecurityTokenDescriptor {
				Issuer = _configuration["JwtValidIssuer"],
				Audience = _configuration["JwtValidAudience"],
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(JwtClaimTypes.Audience,_configuration["JwtValidAudience"]),
					new Claim(JwtClaimTypes.Issuer, _configuration["JwtValidIssuer"]),
					new Claim(JwtClaimTypes.Id, user.Id.ToString()),
					new Claim(JwtClaimTypes.Name, user.Name),
					new Claim(JwtClaimTypes.Email, user.Email),
					new Claim(JwtClaimTypes.Mobile, user.Mobile)
				}),
				Expires = authTime.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			var jwtTokenJson = new {
				access_token = tokenHandler.WriteToken(token),
				token_type = JwtBearerDefaults.AuthenticationScheme,
				profile = new {
					sid = user.Id,
					name = user.Name,
					auth_time = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
					expires_at = new DateTimeOffset(authTime.AddDays(7)).ToUnixTimeSeconds()
				}
			};

			return new CustomResult(1, jwtTokenJson);
		}

		public IActionResult OnPostCheckAuth() {
			if ( !this.User.Identity.IsAuthenticated ) {
				return new CustomResult(0, "Unauthorized");
			}

			return new SuccessResult();
		}
	}
}