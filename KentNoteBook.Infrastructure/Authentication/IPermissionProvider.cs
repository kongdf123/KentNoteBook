using System.Security.Principal;

namespace KentNoteBook.Infrastructure.Authentication
{
	public interface IPermissionProvider
	{
		bool IsUserAuthorized(IPrincipal principal, string permission);
	}
}