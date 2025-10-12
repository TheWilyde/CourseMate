using System.Security.Cryptography;
using System.Text;

namespace CourseMate.Models.HelpingClasses
{
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
