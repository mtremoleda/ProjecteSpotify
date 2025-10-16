
using System.Security.Cryptography;
using System.Text;

namespace ApiSpotify.UTILS
    {
        public static class UtilsContrasenya
        {
            public static string GenerateSalt(int length = 5)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                Random random = new Random();
                return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }

            public static string HashPassword(string password, string salt)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    string saltedPassword = password + salt;
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                    StringBuilder builder = new StringBuilder();
                    foreach (byte b in bytes)
                    {
                        builder.Append(b.ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
        }
    }


