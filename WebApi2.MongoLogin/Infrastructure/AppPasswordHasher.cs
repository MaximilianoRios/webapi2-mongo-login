using System;
using System.Security.Cryptography;
using Microsoft.AspNet.Identity;

namespace WebApi2.MongoLogin.Infrastructure
{
	public class AppPasswordHasher : IPasswordHasher
	{
		private SecurityHelper _securityHelper;
		
		public AppPasswordHasher()
		{
			_securityHelper = new SecurityHelper();
		}

		public string HashPassword(string password)
		{
			return _securityHelper.HashPassword(password);
		}

		public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
		{
			return _securityHelper.HashPassword(providedPassword) == hashedPassword ?
				PasswordVerificationResult.Success : 
				PasswordVerificationResult.Failed;
		}
	}
}
