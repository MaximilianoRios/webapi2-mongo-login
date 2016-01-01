using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApi2.MongoLogin.Data.Shared;
using System.Threading.Tasks;
using WebApi2.MongoLogin.Infrastructure.Identity.Model;

namespace WebApi2.MongoLogin.Data
{

	public class UserRepository : MongoDbContext, IUserRepository
	{
		public UserRepository() : base() {
			
		}
		
		public static UserRepository Create() {
			return new UserRepository();
		}

		public async Task<AppUser> CreateUserAsync(AppUser user)
		{
			DateTime now = DateTime.UtcNow;			
			var coll = _database.GetCollection<AppUser>("users");
			user.Created = now.AddMilliseconds(-now.Millisecond); // Fix a serialization problem on dates with milliseconds
			await coll.InsertOneAsync(user);
			var id = user.Id;
			return user;
		}

		public AppUser GetUser(string id)
		{
			var coll = _database.GetCollection<AppUser>("users");
			var filter = Builders<AppUser>.Filter.Eq("_id", ObjectId.Parse(id));
			var result = coll.Find(filter).FirstOrDefault();
			
			return result;
		}

		public AppUser GetUserByEmail(string email)
		{
			var coll = _database.GetCollection<AppUser>("users");
			var filter = Builders<AppUser>.Filter.Eq("UserName", email);
			var result = coll.Find(filter).FirstOrDefault();
			
			return result;
		}

	}
}