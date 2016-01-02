/*
 * Created by SharpDevelop.
 * User: gabriel.rios
 * Date: 01/01/2016
 * Time: 05:23 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using WebApi2.MongoLogin.Infrastructure.Identity.Model;

namespace WebApi2.MongoLogin.Infrastructure
{
	public class SecurityAuthProvider : OAuthAuthorizationServerProvider
	{
		[Inject]
		public SecurityHelper _securityHelper { get; set; }
			
		public SecurityAuthProvider()
		{
			_securityHelper = new SecurityHelper();
		}
		
		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context) 
		{
			SecurityUserManager userManager = context.OwinContext.Get<SecurityUserManager>("AspNet.Identity.Owin:"
                + typeof(SecurityUserManager).AssemblyQualifiedName);
			
			// string hashedPassword = _securityHelper.HashPassword(context.Password);
			AppUser user = await userManager.FindAsync(context.UserName, context.Password);
			if (user == null)
            {
                context.SetError("invalid_grant", "The username or password is incorrect");
            }
            else
            {            	
                ClaimsIdentity ident = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
               
                AuthenticationTicket ticket = new AuthenticationTicket(ident, new AuthenticationProperties());
                context.Validated(ticket);
                
                context.Request.Context.Authentication.SignIn(ident);
            }			
		}
		
		public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }
	}
}
