using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using WebApi2.MongoLogin.Data;
using WebApi2.MongoLogin.Infrastructure.Identity.Model;

namespace WebApi2.MongoLogin.Infrastructure
{
	public class SecurityUserManager : UserManager<AppUser>
	{
		public SecurityUserManager(IUserStore<AppUser> store) : base(store) {
			
		}
		
		public static SecurityUserManager Create(IdentityFactoryOptions<SecurityUserManager> options, 
		                                         IOwinContext context) {
			var manager = new SecurityUserManager(new UserStore(context.Get<UserRepository>()));
			manager.PasswordHasher = new AppPasswordHasher();
			return manager;
		}
		
	}
}
