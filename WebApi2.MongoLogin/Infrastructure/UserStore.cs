/*
 * Created by SharpDevelop.
 * User: gabriel.rios
 * Date: 01/01/2016
 * Time: 05:20 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNet.Identity;
using WebApi2.MongoLogin.Data;
using WebApi2.MongoLogin.Infrastructure.Identity.Model;

namespace WebApi2.MongoLogin.Infrastructure
{
	public class UserStore : IUserStore<AppUser>, IUserPasswordStore<AppUser>, 
		IUserClaimStore<AppUser>, IUserLoginStore<AppUser>, IUserRoleStore<AppUser>,
		IUserSecurityStampStore<AppUser>
	{
		// private UserRepository _ctx;		
		private UserRepository _ctx;	
		protected bool IsDisposed { get; private set; }
        public bool DisposeRepository { get; set; }
		
		public UserStore(UserRepository context)
		{
			_ctx = context;
			
			DisposeRepository = false;
		}

		public async Task CreateAsync(AppUser user)
		{
			ThrowIfDisposed();
			
			await _ctx.CreateUserAsync(user);
		}

		public Task UpdateAsync(AppUser user)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(AppUser user)
		{
			throw new NotImplementedException();
		}

		public Task<AppUser> FindByIdAsync(string userId)
		{			
			var AppUser = _ctx.GetUser(userId);
			return Task.FromResult(AppUser);
		}

		public Task<AppUser> FindByNameAsync(string userName)
		{
			var AppUser = _ctx.GetUserByEmail(userName);
			return Task.FromResult(AppUser);
		}

			public Task SetPasswordHashAsync(AppUser user, string passwordHash)
			{
				user.PasswordHash = passwordHash;
				return Task.FromResult(0);
			}
			public Task<string> GetPasswordHashAsync(AppUser user)
			{
				return Task.FromResult<string>(user.PasswordHash);
			}
			public Task<bool> HasPasswordAsync(AppUser user)
			{
				return Task.FromResult<bool>(!String.IsNullOrEmpty(user.PasswordHash));
			}

		public Task<System.Collections.Generic.IList<System.Security.Claims.Claim>> GetClaimsAsync(AppUser user)
		{			
			return Task.FromResult<IList<Claim>>(new List<Claim>());
		}


		public Task AddClaimAsync(AppUser user, System.Security.Claims.Claim claim)
		{
			return Task.FromResult<int>(0);
		}


		public Task RemoveClaimAsync(AppUser user, System.Security.Claims.Claim claim)
		{
			 return Task.FromResult<int>(0);
		}

		public Task AddLoginAsync(AppUser user, UserLoginInfo login)
		{
			user.AssignLogin(login.LoginProvider, login.ProviderKey);
			return Task.FromResult(0);
			
		}
		public Task RemoveLoginAsync(AppUser user, UserLoginInfo login)
		{
			user.RemoveLogin(login.LoginProvider, login.ProviderKey);
			return Task.FromResult(0);
		}
		
		public Task<IList<UserLoginInfo>> GetLoginsAsync(AppUser user)
		{
			IList<UserLoginInfo> logins = user.HasLogins()
                ? user.Logins.Select(i => new UserLoginInfo(i.LoginProvider, i.ProviderKey)).ToList()
                : new List<UserLoginInfo>();

            return Task.FromResult(logins);

		}
		public Task<AppUser> FindAsync(UserLoginInfo login)
		{
			throw new NotImplementedException();
		}

		public Task AddToRoleAsync(AppUser user, string roleName)
		{
			user.AssignRole(roleName);
            return Task.FromResult(0);
		}


		public Task RemoveFromRoleAsync(AppUser user, string roleName)
		{
            ThrowIfDisposed();
            
            user.RemoveRole(roleName);
            return Task.FromResult(0);
		}


		public Task<IList<string>> GetRolesAsync(AppUser user)
		{
            IList<string> roles = user.Roles ?? new List<string>();

            return Task.FromResult(roles);
		}

		public Task<bool> IsInRoleAsync(AppUser user, string role)
		{
			
            return Task.FromResult(user.HasRole(role));
		}

		public Task SetSecurityStampAsync(AppUser user, string stamp)
		{
			user.SecurityStamp = stamp;
			return Task.FromResult(0);
		}
		public Task<string> GetSecurityStampAsync(AppUser user)
		{
			return Task.FromResult(user.SecurityStamp);
		}
		
		#region Dispose section
		public void Dispose()
		{
			// Not really necessary but the implementation is mandatory			
            Dispose(true);
            GC.SuppressFinalize(this);
		}
		public void Dispose(bool disposing) {
			ThrowIfDisposed();
			
			IsDisposed = true;
			
			if(disposing && DisposeRepository && _ctx != null) {
				_ctx.Dispose();
				_ctx = null;
			}
		}
		
		protected virtual void ThrowIfDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);
        }
		#endregion
	}
}
