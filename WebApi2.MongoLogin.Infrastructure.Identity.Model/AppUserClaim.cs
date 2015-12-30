/*
 * Created by SharpDevelop.
 * User: gabriel.rios
 * Date: 30/12/2015
 * Time: 01:02 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace WebApi2.MongoLogin.Infrastructure.Identity.Model
{
	/// <summary>
	/// Description of AppUserClaim.
	/// </summary>
	public class AppUserClaim
	{
		public AppUserClaim()
		{
		}
		
		public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
	}
}
