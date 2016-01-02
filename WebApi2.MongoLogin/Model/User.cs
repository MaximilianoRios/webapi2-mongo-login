using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi2.MongoLogin.Model
{
	public class User
	{
		public string Id { get; set; }
		// [Required]
		// [EmailAddress]
		// public string Email { get; set; }
		[Required]
		public string UserName { get; set; }
		[Required]
		public string FullName { get; set; }
		[Required]
		public DateTime BirthDate { get; set; }
		[Required]
		public string Password { get; set; }
		// public UserStatus Status { get; set; }
		public DateTime? Created { get; set; }
		public DateTime? LastUpdated { get; set; }
	}
}
