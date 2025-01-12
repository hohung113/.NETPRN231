using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Helper
    {
        public static string? GetString(string text)
        {

            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", true, true)
              .Build();
            string? connectionString = null;
            switch (text)
            {
                case "DataSource":
                    connectionString = config["ConnectionStrings:eStore"];
                    break;
                case "EmailAdmin":
                    connectionString = config["AdminAccount:Email"];
                    break;
                case "PassAdmin":
                    connectionString = config["AdminAccount:Password"];
                    break;
                default:
                    break;
            }
            return connectionString;
        }

        // Password
        private const int IterationCount = 10000;
        private const int SaltSize = 16;
        private const int HashSize = 32;
        public static string HashPassword(string password)
        {
    
            byte[] salt = GenerateSalt();
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, IterationCount))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                byte[] hashBytes = new byte[salt.Length + hash.Length];

                Array.Copy(salt, 0, hashBytes, 0, salt.Length);
                Array.Copy(hash, 0, hashBytes, salt.Length, hash.Length);

                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool VerifyPassword(string storedHash, string password)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, salt.Length);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, IterationCount))
            {
                byte[] newHash = pbkdf2.GetBytes(HashSize); 
                                                           
                for (int i = 0; i < HashSize; i++)
                {
                    if (hashBytes[i + salt.Length] != newHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltSize];
                rng.GetBytes(salt); 
                return salt;
            }
        }
    }
}
