using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KentNoteBook.Core;
using KentNoteBook.Infrastructure.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KentNoteBook.WebApp.Pages.Home
{
	public class ProfileModel : PageModel
	{
		readonly KentNoteBookDbContext _db;

		public ProfileModel(KentNoteBookDbContext db) {
			this._db = db;
		}

		public Guid? Id { get; set; }

		[FromForm, BindProperty]
		public UserModel Data { get; set; }

		public class UserModel
		{
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

			this.Id = this.User.Claims
				.Where(x => x.Type == "Id")
				.Select(x => new Guid(x.Value))
				.SingleOrDefault();

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

			return Page();
		}

		public async Task<IActionResult> OnPostSaveAsync() {

			if ( !ModelState.IsValid ) {
				return ModelState.ToJsonResult();
			}

			this.Id = this.User.Claims
				.Where(x => x.Type == "Id")
				.Select(x => new Guid(x.Value))
				.SingleOrDefault();

			var entity = await _db.Users
				.Where(x => x.Id == this.Id)
				.SingleOrDefaultAsync();

			//entity.Name = this.Data.Name;
			entity.NickName = this.Data.NickName;
			entity.Email = this.Data.Email;
			entity.Mobile = this.Data.Mobile;
			entity.Avatar = this.Data.Avatar;
			//entity.IsActive = this.Data.IsActive;

			entity.UpdatedBy = this.User.Identity.Name;
			entity.UpdatedDate = DateTime.Now;

			await _db.SaveChangesAsync();

			return new SuccessResult();
		}
	}
}