/*
 * Created by SharpDevelop.
 * User: gabriel.rios
 * Date: 01/01/2016
 * Time: 10:58 a.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Configuration;

namespace WebApi2.MongoLogin.Data.Configuration
{
	public class MongoDbConfiguration : ConfigurationSection
	{
		public MongoDbConfiguration()
		{
		}
		
		[ConfigurationProperty("serverHost", IsRequired=true)]
		public string ServerHost { 
			get {
				return this["serverHost"].ToString();
			}
			set {
				this["serverHost"] = value;
			}
		}
		
		[ConfigurationProperty("serverPort", IsRequired=true)]
		public int ServerPort { 
			get {
				return (int)this["serverPort"];
			}
			set {
				this["serverPort"] = value;
			}
		}
		
		[ConfigurationProperty("useSsl", IsRequired=false, DefaultValue=false)]
		public bool UseSsl { 
			get {
				return (bool)this["useSsl"];
			}
			set {
				this["useSsl"] = value;
			}
		}
		
		[ConfigurationProperty("databaseName", IsRequired=false)]
		public string DatabasName {
			get {
		 		return this["databaseName"].ToString();
			}
		 	set {
		 		this["databaseName"] = value;
		 	}
		}
	}
}
