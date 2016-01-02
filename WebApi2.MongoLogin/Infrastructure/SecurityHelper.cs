

using System.Security.Cryptography;
using System.Text;
namespace WebApi2.MongoLogin.Infrastructure
{
	public class SecurityHelper
	{
		public SecurityHelper()
		{
		}
		
		public string HashPassword(string password)
		{
			MD5 md5 = MD5.Create();
    		byte[] inputBytes = System.Text.Encoding.Unicode.GetBytes(password);
    		byte[] hash = md5.ComputeHash(inputBytes);
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
			    sb.Append(hash[i].ToString("X2"));
			}
			return sb.ToString().ToLower();
		}
	}
}
