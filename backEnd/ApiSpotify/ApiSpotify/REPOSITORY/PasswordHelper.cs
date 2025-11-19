using System.Security.Cryptography;

namespace ApiSpotify.REPOSITORY
{
    public class PasswordHelper
    {
        public static bool VerifyPassword(string passwordWpf, string hashBase64BD, string saltBase64BD)
        {
            // Convertim strings Base64 de la BD → byte[]
            byte[] saltBytes = Convert.FromBase64String(saltBase64BD);
            byte[] hashBD = Convert.FromBase64String(hashBase64BD);

            // Tornem a generar el hash amb la password que ha escrit l'usuari
            using var pbkdf2 = new Rfc2898DeriveBytes(passwordWpf, saltBytes, 100000, HashAlgorithmName.SHA256);
            byte[] computedHash = pbkdf2.GetBytes(32); // 32 bytes = 256 bits

            // Compare hash generat amb hash de la BD
            return computedHash.SequenceEqual(hashBD);
        }
    }
}
