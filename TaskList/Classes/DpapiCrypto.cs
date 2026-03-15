using System;
using System.Security.Cryptography;
using System.Text;


namespace Test
{
    public static class DpapiCrypto
    {
        // Optional: set a static entropy salt for your app (not a secret, but helps discourage cross-app reuse)
        private static readonly byte[] Entropy = Encoding.UTF8.GetBytes("MyApp-Config-Entropy-v1");

        public static string ProtectToBase64(string plainText, DataProtectionScope scope)
        {
            if (plainText == null) return null;
            var data = Encoding.UTF8.GetBytes(plainText);
            var protectedBytes = ProtectedData.Protect(data, Entropy, scope);
            return Convert.ToBase64String(protectedBytes);
        }

        public static string UnprotectFromBase64(string base64CipherText, DataProtectionScope scope)
        {
            if (string.IsNullOrWhiteSpace(base64CipherText)) return null;
            var protectedBytes = Convert.FromBase64String(base64CipherText);
            var data = ProtectedData.Unprotect(protectedBytes, Entropy, scope);
            return Encoding.UTF8.GetString(data);
        }
    }

}
