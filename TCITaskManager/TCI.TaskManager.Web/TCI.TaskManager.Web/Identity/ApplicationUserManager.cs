using TCI.TaskManager.Web.Core.Models;
using Microsoft.AspNet.Identity;

namespace TCI.TaskManager.Web.Identity
{
	public class ApplicationUserManager : UserManager<User>
	{
		public ApplicationUserManager(IUserStore<User> store)
			: base(store)
		{
			UserValidator = new UserValidator<User>(this)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};
		}
	}
}