using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi2.MongoLogin.Infrastructure.Identity.Model;

namespace WebApi2.MongoLogin.Data.Shared
{
	public interface IUserRepository
	{
		Task<AppUser> CreateUserAsync(AppUser user);
		AppUser GetUser(string id);
		AppUser GetUserByEmail(string email);
	}
}