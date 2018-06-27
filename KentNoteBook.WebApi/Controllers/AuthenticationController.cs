using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using KentNoteBook.Data;
using KentNoteBook.Service.Common;
using KentNoteBook.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace KentNoteBook.Service.Controllers
{
	[Route("api/[controller]")]
	[AllowAnonymous]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		public AuthenticationController(KentNoteBookDbContext db, IConfiguration configuration) {
			_db = db;
			_configuration = configuration;
		}

		readonly KentNoteBookDbContext _db;
		readonly IConfiguration _configuration;

		[HttpPost("Token")]
		public IActionResult Token(LoginModel login) {
			if ( !ModelState.IsValid ) {
				return BadRequest(ModelState);
			}

			var user = _db.Users
				.AsNoTracking()
				.Where(x => x.Name == login.UserName)
				.Where(x => x.Password == login.Password)
				.Where(x => x.IsActive)
				.Where(x => x.Status == Status.Enabled)
				.SingleOrDefault();
			if ( user == null ) {
				return Unauthorized();
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["SecurityKey"]);

			var authTime = DateTime.UtcNow;
			var expiredAt = authTime.AddDays(7);

			var tokenDescriptor = new SecurityTokenDescriptor {
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(JwtClaimTypes.Audience,"api"),
					new Claim(JwtClaimTypes.Issuer,"http://localhost:10745"),
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
			return Ok(new {
				access_token = tokenString,
				token_type = "Bearer",
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