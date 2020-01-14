using QnA.Application.Interfaces.Security;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace QnA.Security
{
    public class HashGenerator : IHashGenerator
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;
        public bool CheckHash(string hash, string password)
        {
            var parts = hash.Split('.');

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format. " +
                  "Should be formatted as `{iterations}.{salt}.{hash}`");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);


            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              salt,
              iterations))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);

                var verified = keyToCheck.SequenceEqual(key);

                return verified;
            }
        }

        public string ComputeHash(string value)
        {

            using (var algorithm = new Rfc2898DeriveBytes(value, SaltSize, Iterations))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{Iterations}.{salt}.{key}";
            }
        }
    }
}
