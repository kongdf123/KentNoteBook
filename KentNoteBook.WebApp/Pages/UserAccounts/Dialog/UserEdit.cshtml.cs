using System;
using System.ComponentModel.DataAnnotations;
using KentNoteBook.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KentNoteBook.WebApp.Pages.UserAccounts.Dialog
{
	public class UserEditModel : PageModel
	{
		readonly KentNoteBookDbContext _db;
		public UserEditModel(KentNoteBookDbContext db) {
			this._db = db;
		}

		[FromForm, BindProperty]
		public UserModel Data { get; set; }

		public class UserModel
		{
			public Guid Id { get; set; }

			[Required]
			public string Name { get; set; }

			[StringLength(30)]
			public string NickName { get; set; }

			[StringLength(50), Required]
			public string Password { get; set; }

			[StringLength(100), Required]
			public string Email { get; set; }

			[StringLength(30)]
			public string Mobile { get; set; }

			[StringLength(150)]
			public string Avatar { get; set; }

			[StringLength(500)]
			public string Discription { get; set; }

			public bool IsActive { get; set; }

			public Status Status { get; set; }
		}

		public void OnGet() {

		}
	}
}