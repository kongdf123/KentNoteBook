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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace KentNoteBook.WebApp.Pages
{
	[AllowAnonymous]
	public class LoginModel : PageModel
	{
		public LoginModel(KentNoteBookDbContext db, IConfiguration configuration) {
			_db = db;
			_configuration = configuration;
		}

		readonly KentNoteBookDbContext _db;
		readonly IConfiguration _configuration;

		[FromForm, BindProperty]
		public UserModel User { get; set; }

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

			this.User = new UserModel();

			return Page();
		}

		public async Task<IActionResult> OnPostLoginAsync() {
			if ( !ModelState.IsValid ) {
				return BadRequest(ModelState);
			}

			var user = await _db.Users
				.AsNoTracking()
				.Where(x => x.Name == User.UserName)
				.Where(x => x.Password == User.Password)
				.Where(x => x.IsActive)
				.Where(x => x.Status == Status.Enabled)
				.SingleOrDefaultAsync();
			if ( user == null ) {
				return Unauthorized();
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["JwtSecurityKey"]);

			var authTime = DateTime.UtcNow;
			var expiredAt = authTime.AddDays(7);

			var tokenDescriptor = new SecurityTokenDescriptor {
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(JwtClaimTypes.Audience,_configuration["JwtAudience"]),
					new Claim(JwtClaimTypes.Issuer, _configuration["JwtValidIssuer"]),
					new Claim(JwtClaimTypes.Id, user.Id.ToString()),
					new Claim(JwtClaimTypes.Name, user.Name),
					new Claim(JwtClaimTypes.Email, user.Email),
					new Claim(JwtClaimTypes.Mobile, user.Mobile)
				}),
				Expires = expiredAt,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);
			return new JsonResult(new {
				access_token = tokenString,
				token_type = JwtBearerDefaults.AuthenticationScheme,
				profile = new {
					sid = user.Id,
					name = user.Name,
					auth_time = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
					expires_at = new DateTimeOffset(expiredAt).ToUnixTimeSeconds()
				}
			});
		}
	}
}