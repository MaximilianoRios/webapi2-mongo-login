using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApi2.MongoLogin.Infrastructure.Identity.Model
{
	/// <summary>
	/// Represents an application user
	/// </summary>
	public class AppUser : IUser<string>
	{
		[BsonId]
		public ObjectId _id { get; set; }
		
		/// <summary>
		/// The id is just the representation of ObjectID
		/// </summary>
		[BsonIgnore]
		public string Id
		{ 
			get 
			{
				if(_id == BsonNull.Value) {
					return string.Empty;
				}
				return _id.ToString();
			}
		}
		public string UserName { get; set; }		
		[Required]
		public string FullName { get; set; }
		[Required]
		[BsonDateTimeOptions(DateOnly = true)]
		public DateTime BirthDate { get; set; }
		public string PasswordHash  { get; set; }
		public string SecurityStamp { get; set; }
		public DateTime? Created { get; set; }
		public DateTime? LastUpdated { get; set; }		
		
		public List<string> Roles { get; set; }
		public List<AppUserClaim> Claims { get; set; }
		public List<AppUserLogin> Logins { get; set; }

		public AppUser() : base() 
		{
			Roles = new List<string>();
            Logins = new List<AppUserLogin>();
            Claims = new List<AppUserClaim>();			
		}
		
		public AppUser(string userName) : this()
		{
			UserName = userName;
		}
		public AppUser(string id, string userName) : this(userName)
		{
			_id = ObjectId.Parse(id);
		}

		public virtual void AssignRole(string role)
        {
            if (!HasRole(role))
                Roles.Add(role);
        }

        public virtual void RemoveRole(string role)
        {
            if (HasRoles())
                Roles.RemoveAll(i =>
                    i.Equals(role, StringComparison.OrdinalIgnoreCase));
        }

        public virtual bool HasRole(string role)
        {
            return HasRoles() && Roles.Any(i =>
                i.Equals(role, StringComparison.OrdinalIgnoreCase));
        }

        public virtual bool HasRoles()
        {
            return Roles != null && Roles.Any();
        }

        public virtual void AssignLogin(string loginProvider, string providerKey)
        {
            if (!HasLogin(loginProvider, providerKey))
                Logins.Add(new AppUserLogin
                {
                    LoginProvider = loginProvider,
                    ProviderKey = providerKey
                });
        }

        public virtual void RemoveLogin(string loginProvider, string providerKey)
        {
            if (HasLogins())
                Logins.RemoveAll(x =>
                    x.LoginProvider.Equals(loginProvider, StringComparison.OrdinalIgnoreCase) &&
                    x.ProviderKey.Equals(providerKey, StringComparison.OrdinalIgnoreCase));
        }

        public virtual bool HasLogin(string loginProvider, string providerKey)
        {
            return HasLogins() && Logins.Any(i =>
                i.LoginProvider.Equals(loginProvider, StringComparison.OrdinalIgnoreCase) &&
                i.ProviderKey.Equals(providerKey, StringComparison.OrdinalIgnoreCase));
        }

        public virtual bool HasLogins()
        {
            return Logins != null && Logins.Any();
        }

        public virtual void AssignClaim(string claimType, string claimValue)
        {
            if (!HasClaim(claimType, claimValue))
                Claims.Add(new AppUserClaim
                {
                    ClaimType = claimType,
                    ClaimValue = claimValue
                });
        }

        public virtual void RemoveClaim(string claimType, string claimValue)
        {
            if (HasClaims())
                Claims.RemoveAll(x =>
                    x.ClaimType.Equals(claimType, StringComparison.OrdinalIgnoreCase) &&
                    x.ClaimValue.Equals(claimValue, StringComparison.OrdinalIgnoreCase));
        }

        public virtual bool HasClaim(string claimType, string claimValue)
        {
            return HasClaims() && Claims.Any(i =>
                i.ClaimType.Equals(claimType, StringComparison.OrdinalIgnoreCase) &&
                i.ClaimValue.Equals(claimValue, StringComparison.OrdinalIgnoreCase));
        }

        public virtual bool HasClaims()
        {
            return Claims != null && Claims.Any();
        }		
	}
}
