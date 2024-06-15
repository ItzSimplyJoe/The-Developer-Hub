using System.Security.Cryptography;
using System.Text;

namespace DeveloperHub.Components.Pages.Account
{
    public static class AccountHelpers
    {
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }

        public static string ToDataUrl(this MemoryStream data, string format)
        {
            var span = new Span<byte>(data.GetBuffer()).Slice(0, (int)data.Length);
            return $"data:{format};base64,{Convert.ToBase64String(span)}";
        }

        public static byte[] ToBytes(string url)
        {
            var commaPos = url.IndexOf(',');
            if (commaPos < 0) 
                return null;
            var base64 = url.Substring(commaPos + 1);
            return Convert.FromBase64String(base64);

        }
    }

    public static class PasswordValidator
    {
        public static bool ValidatePassword(string? password)
        {
            if (password is { Length: < 8 })
            {
                return false;
            }

            if (password != null && !password.Any(char.IsUpper))
            {
                return false;
            }

            if (password != null && !password.Any(char.IsLower))
            {
                return false;
            }

            return password != null && password.Any(char.IsDigit) && password.Any(IsSpecialCharacter);
        }

        private static bool IsSpecialCharacter(char c)
        {
            return !char.IsLetterOrDigit(c);
        }
    }
}
