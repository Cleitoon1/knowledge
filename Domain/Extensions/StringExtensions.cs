using System.Text;

namespace Domain.Extensions
{
    public static class StringExtension
    {
        public static string ConvertToMD5(this string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            var password = text += "|232217c1-0916-4780-bf85-fda18f99e700";
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(password));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }
    }
}
