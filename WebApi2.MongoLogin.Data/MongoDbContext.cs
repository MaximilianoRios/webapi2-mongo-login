/*
 * Created by SharpDevelop.
 * User: gabriel.rios
 * Date: 01/01/2016
 * Time: 10:56 a.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using MongoDB.Driver;
using WebApi2.MongoLogin.Data.Configuration;

namespace WebApi2.MongoLogin.Data
{
	/// <summary>
	/// This is the class that creates a MongoDB Context similar to the concept of Entity Framework
	/// that hides the implementation under a class that you should inherit to use the database
	/// The context uses a Configuration class to get the details of the connection
	/// </summary>
	public class MongoDbContext
	{
		protected static IMongoClient _client;
		protected static IMongoDatabase _database;
		private string _dbName;

		public MongoDbContext() {
			// Create the client in the default constructor
			var config = 
				(MongoDbConfiguration)System.Configuration.ConfigurationManager.GetSection("NoSqlDbConfiguration");			
			MongoClientSettings settings = new MongoClientSettings() {
				Server = new MongoServerAddress(config.ServerHost, config.ServerPort)
			};
			settings.UseSsl = config.UseSsl;
			_client = new MongoClient(settings);
			_dbName = config.DatabasName;
			_database = _client.GetDatabase(_dbName);
		}
		
		public MongoDbContext(string db) : this()
		{
			// Create the database in this constructor if the database name
			// has been sent to override the default name in the constructor
			_database = _client.GetDatabase(db);
		}
		
		
		
		public void Dispose()
		{
			// Just to implement the interface
		}
	}
}
