using QnA.Application.Interfaces.Security;
using System.Security.Cryptography;
using System.Text;

namespace QnA.Security
{
    public class HashGenerator : IHashGenerator
    {
        public string ComputeHash(string value)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(value));
                return Encoding.ASCII.GetString(result);
            }
        }
    }
}
