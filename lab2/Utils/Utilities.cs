using System.Security.Cryptography;
using System.Text;

namespace lab2.Utils
{
    public static class Utilities
    {
        public static string GenerateHash(string input)
        {
            var salt = "salt414@32432@$%#323@5";
            var raw = input + salt;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(raw));
                StringBuilder result = new StringBuilder();
                foreach (var b in bytes)
                    result.Append(b.ToString("x2"));

                return result.ToString();
            }
        }
    }
}
