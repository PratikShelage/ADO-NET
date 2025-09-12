using System.Security.Cryptography;

namespace TestAppProject.Helper
{
    public class PasswordHashing
    {
        private static RSACryptoServiceProvider rng = new RSACryptoServiceProvider();

        private static readonly int SaltSize = 16;
        private static readonly int HashSize = 32;
        private static readonly int Iterations = 100000;

        private static readonly HashAlgorithmName algorithm = HashAlgorithmName.SHA512;

        public static string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, algorithm, HashSize);

            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";

        }

        public static bool VerifyPassword(string password, string passwordHash)
        {
            string[] parts = passwordHash.Split("-");
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, algorithm, HashSize);

            //return inputHash.SequenceEqual(hash);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }
}

