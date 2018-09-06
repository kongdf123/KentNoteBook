using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KentNoteBook.Core;
using KentNoteBook.Core.Entities;
using KentNoteBook.Infrastructure.Utility;

namespace KentNoteBook.Infrastructure
{
	public static class KentNoteBookDbInitializer
	{
		public static void SeedData(this KentNoteBookDbContext db) {
			if ( !db.Users.Any() ) {

				var saltString = Crypto.GeneratePasswordSalt();

				db.Users.Add(new SystemUser {
					Id = Guid.NewGuid(),
					Name = "Admin",
					NickName = "Admin",
					PasswordSalt = saltString,
					Password = Crypto.HashPassword(saltString, "123456"),
					Email = "admin@admin.com",
					Mobile = "13811111111",
					IsActive = true,
					Status = Status.Enabled
				});

				for ( int i = 0; i < 30; i++ ) {
					saltString = Crypto.GeneratePasswordSalt();
					db.Users.Add(new SystemUser {
						Id = Guid.NewGuid(),
						Name = "User" + i,
						NickName = "User " + i,
						PasswordSalt = saltString,
						Password = Crypto.HashPassword(saltString, "123456"),
						Email = "User@User.com",
						Mobile = "13811111111",
						IsActive = true,
						Status = Status.Enabled
					});
				}
			}

			db.SaveChanges();
		}
	}
}
