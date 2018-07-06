using System;
using System.Collections.Generic;
using System.Text;

namespace KentNoteBook.Infrastructure.Authentication
{
	public class JwtRequestHeader
	{
		public TokenGrantType GrantType { get; set; }
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string RefreshToken { get; set; }
	}

	public enum TokenGrantType
	{
		Password = 1,
		RefreshToken = 2,
	}
}
