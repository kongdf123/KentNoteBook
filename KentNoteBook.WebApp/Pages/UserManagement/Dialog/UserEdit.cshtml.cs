using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KentNoteBook.Data;
using KentNoteBook.Infrastructure.Mvc;
using KentNoteBook.Infrastructure.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KentNoteBook.WebApp.Pages.UserAccounts.Dialog
{
	[AllowAnonymous]
	public class UserEditModel : PageModel
	{
		readonly KentNoteBookDbContext _db;

		public UserEditModel(KentNoteBookDbContext db) {
			this._db = db;
		}

		[BindProperty(SupportsGet = true)]
		public Guid? Id { get; set; }

		[FromForm, BindProperty]
		public UserModel Data { get; set; }

		public class UserModel
		{
			[Required]
			public string Name { get; set; }

			[StringLength(30)]
			public string NickName { get; set; }

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

		public async Task<IActionResult> OnGetAsync() {

			if ( !this.Id.HasValue ) {
				this.Data = new UserModel();
				this.Id = Guid.NewGuid();
			}
			else {
				this.Data = await _db.Users
					.AsNoTracking()
					.Where(x => x.Id == this.Id)
					.Select(x => new UserModel {
						Name = x.Name,
						NickName = x.NickName,
						Email = x.Email,
						Mobile = x.Mobile,
						Avatar = x.Avatar,
						Discription = x.Discription,
						IsActive = x.IsActive,
						Status = x.Status
					})
					.SingleOrDefaultAsync();

				if ( this.Data == null ) {
					return new BadRequestResult();
				}

			}

			return Page();
		}

		public async Task<IActionResult> OnPostSaveAsync() {

			System.Threading.Thread.Sleep(2000);

			if ( !ModelState.IsValid ) {
				return ModelState.ToJsonResult();
			}

			var entity = await _db.Users
				.Where(x => x.Id == this.Id)
				.SingleOrDefaultAsync();

			if ( entity == null ) {

				var saltString = Crypto.GeneratePasswordSalt();

				entity = new Data.Entities.SystemUser {
					Id = this.Id ?? Guid.NewGuid(),

					PasswordSalt = saltString,
					Password = Crypto.HashPassword(saltString, "123456"),

					Status = Status.Enabled,

					CreatedBy = "Admin",
					CreatedDate = DateTime.Now,
				};

				_db.Users.Add(entity);
			}

			entity.Name = this.Data.Name;
			entity.NickName = this.Data.NickName;
			entity.Email = this.Data.Email;
			entity.Mobile = this.Data.Mobile;
			entity.IsActive = this.Data.IsActive;

			entity.UpdatedBy = "Admin";
			entity.UpdatedDate = DateTime.Now;

			await _db.SaveChangesAsync();

			return new SuccessResult();
		}
	}
}