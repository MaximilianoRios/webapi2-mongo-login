using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using WebApi2.MongoLogin.Infrastructure;
using WebApi2.MongoLogin.Infrastructure.Identity.Model;
using WebApi2.MongoLogin.Model;

namespace WebApi2.MongoLogin
{
	public class UsersController  : ApiController
	{
		private SecurityUserManager _userManager;
		private AppPasswordHasher _passwordHasher;
		
		public SecurityUserManager UserManager
        {
            get
            {
            	Console.WriteLine("Getting UserManager from Context");
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<SecurityUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
		
		public UsersController()
		{
			_passwordHasher = new AppPasswordHasher();
		}
		
		// [Authorize]
    	public async Task<IHttpActionResult> GetUser(string id) {
			Console.WriteLine("Get user {0}", id);
    		var appUser = await UserManager.FindByIdAsync(id);
    		User user = new User {
    			UserName = appUser.UserName,
    			FullName = appUser.FullName,
    			BirthDate = DateTime.SpecifyKind(appUser.BirthDate, DateTimeKind.Utc),
    			Created = appUser.Created,
    			Id = appUser.Id
    		};
    		return user == null ?
    			(IHttpActionResult)BadRequest("No user found") :
    			Ok(user);
    	}
		
		public async Task<HttpResponseMessage> PostUser(User user) {
    		if(user != null && ModelState.IsValid) {        		
				var appUser = new AppUser {
					UserName = user.UserName,
					BirthDate = GetUtcDateTime(user.BirthDate),
					FullName = user.FullName,
					PasswordHash = _passwordHasher.HashPassword(user.Password)
				};
				var result = await UserManager.CreateAsync(appUser);
				
				user.Id = appUser.Id;
				user.Password = string.Empty;
				user.Created = appUser.Created;
				user.BirthDate = appUser.BirthDate;
				
				var response = Request.CreateResponse(HttpStatusCode.Created, user);
    			response.Headers.Add("Location", GetUserUri(appUser.Id));
    			return response;
    		}
        	return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
    	}
    	
		/// <summary>
		/// Build the current users location to conform the RESTFul principle
		/// </summary>
		/// <param name="id">User ID</param>
		/// <returns>URL /api/users/{id}</returns>
		private string GetUserUri(string id)
		{
			return String.Format("/api/users/{0}", id);
		}

		DateTime GetUtcDateTime(DateTime dateTime)
		{
			var utcDateTime = DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc);
			return utcDateTime;
		}
	}
}
