using System;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using WebApi2.MongoLogin.Data;
using WebApi2.MongoLogin.Infrastructure;

[assembly: OwinStartup(typeof(WebApi2.MongoLogin.IdentityConfig))]
namespace WebApi2.MongoLogin
{
	public class IdentityConfig
	{
		public void Configuration(IAppBuilder app)
        {
			// app.CreatePerOwinContext<UserRepository>(UserRepository.Create);
			app.CreatePerOwinContext<UserRepository>(UserRepository.Create);
			
			app.CreatePerOwinContext<SecurityUserManager>(SecurityUserManager.Create);
			
			app.UseOAuthBearerTokens(new OAuthAuthorizationServerOptions
            {
                Provider = new SecurityAuthProvider(),
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Authentication")
            });
		}		
	}
}
