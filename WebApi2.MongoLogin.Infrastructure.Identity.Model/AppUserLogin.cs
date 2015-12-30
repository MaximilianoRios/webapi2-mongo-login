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
	/// Description of AppUserLogin.
	/// </summary>
	public class AppUserLogin
	{
		public AppUserLogin()
		{
		}
		
		public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
	}
}
