using System;
using System.Security.Cryptography;
using System.Text;

namespace ApiSpotify.UTILS
{
    public static class UtilsContrasenya
    {
        
        public static string GenerateSalt(int size = 16)
        {
            byte[] salt = new byte[size];
            RandomNumberGenerator.Fill(salt);
            return Convert.ToBase64String(salt);
        }

        public static string HashPassword(string password, string saltBase64)
        {
            byte[] salt = Convert.FromBase64String(saltBase64);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            return Convert.ToBase64String(hash);
        }

        public static bool VerifyPassword(string password, string hashBase64, string saltBase64)
        {
            return HashPassword(password, saltBase64) == hashBase64;
        }


    }
}



