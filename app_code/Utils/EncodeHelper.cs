using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

public static class EncodeHelper
{
    public static string EncodeBase64(this string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }
    public static string DecodeBase64(this string str64)
    {
        byte[] decodedBytes = Convert.FromBase64String(str64);
        return Encoding.UTF8.GetString(decodedBytes);
    }
    public static string EnCoding(this string str)
    {
        if (string.IsNullOrEmpty(str)) return string.Empty;
        UnicodeEncoding encoding = new UnicodeEncoding();
        byte[] hashBytes = encoding.GetBytes(str);

        SHA1 sha1 = new SHA1CryptoServiceProvider();
        byte[] cryptPassword = sha1.ComputeHash(hashBytes);
        return BitConverter.ToString(cryptPassword);
    }
    public static bool IsBase64String(this string s)
    {
        if (string.IsNullOrEmpty(s))
            return false;

        s = s.Trim();
        return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
    }
    public static string CreateMD5(string input)
    {
        // Use input string to calculate MD5 hash
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
    public static Guid ConvertToMd5HashGUID(string value)
    {
        // convert null to empty string - null can not be hashed
        if (value == null)
            value = string.Empty;

        // Use input string to calculate MD5 hash
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.Unicode.GetBytes(value);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // convert the hash to a Guid
            return new Guid(hashBytes);
        }
    }
    public static string Encrypt(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        var bytesToBeEncrypted = Encoding.UTF8.GetBytes(text);
        var passwordBytes = Encoding.UTF8.GetBytes("SLKCORP".EnCoding());

        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

        byte[] bytesEncrypted = EncodeHelper.Encrypt(bytesToBeEncrypted, passwordBytes);
        return Convert.ToBase64String(bytesEncrypted);
    }
    public static string Decrypt(this string text)
    {
        if (!text.IsBase64String())
            return text;

        try
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            byte[] bytesToBeDecrypted = Convert.FromBase64String(text);

            var passwordBytes = Encoding.UTF8.GetBytes("SLKCORP".EnCoding());

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            var bytesDecrypted = EncodeHelper.Decrypt(bytesToBeDecrypted, passwordBytes);

            return Encoding.UTF8.GetString(bytesDecrypted);
        }
        catch { return text; }
    }
    private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
    {
        byte[] encryptedBytes = null;

        var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        using (MemoryStream ms = new MemoryStream())
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                AES.KeySize = 256;
                AES.BlockSize = 128;
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);

                AES.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                    cs.Close();
                }

                encryptedBytes = ms.ToArray();
            }
        }

        return encryptedBytes;
    }
    private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
    {
        byte[] decryptedBytes = null;

        // Set your salt here, change it to meet your flavor:
        // The salt bytes must be at least 8 bytes.
        var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        using (MemoryStream ms = new MemoryStream())
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                AES.KeySize = 256;
                AES.BlockSize = 128;
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);
                AES.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                    cs.Close();
                }

                decryptedBytes = ms.ToArray();
            }
        }

        return decryptedBytes;
    }
}