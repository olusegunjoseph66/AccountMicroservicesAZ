using System.Security.Cryptography;
using System.Text;

namespace Shared.Utilities.Helpers
{
    public static class EncryptionHelper
    {
        private const string secretKey = "d69iNivDmHLpUA223sqsfhqGbMR7m#ld9yo6d01040d1fbbf232e7MmY1YQ==";

        public static string Hash(string text)
        {
            var clearBytes = Encoding.UTF8.GetBytes(text);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new(secretKey,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                if (encryptor != null)
                {
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using MemoryStream ms = new();
                    using (CryptoStream cs = new(ms, encryptor.CreateEncryptor(),
                        CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }

            return string.Empty;
        }

        private static string DecryptString(string encryptedText)
        {
            string decryptedValue = string.Empty;

            byte[] cipherBytes = Convert.FromBase64String(encryptedText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new(secretKey,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                if (encryptor != null)
                {
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using MemoryStream ms = new();
                    using (CryptoStream cs = new(ms, encryptor.CreateDecryptor(),
                        CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }

                    decryptedValue = Encoding.UTF8.GetString(ms.ToArray());
                }
            }

            return decryptedValue;
        }

        public static bool Verify(string plainText, string encryptedText)
        {
            string result = DecryptString(encryptedText);
            return result == plainText;
        }
    }
}
