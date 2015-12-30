using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace WebApi2.MongoLogin.Infrastructure.Identity.Model
{
	/// <summary>
	/// Represents an application role
	/// </summary>
	public class AppRole : IRole<string>
	{
		public AppRole() : base() { }
        public AppRole(string name)  
        {
        	Name = name;
		}
        public AppRole(string id, string name) {
        	Id = id;
        	Name = name;
        }
        
        public string Id { get; set; }
        public string Name { get; set; }
	}
}