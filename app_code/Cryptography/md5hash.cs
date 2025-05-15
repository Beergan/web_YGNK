using System;
using System.Security.Cryptography;
using System.Text;

public class md5hash
{
    public string mahoamd5(string txt)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] result = md5.ComputeHash(Encoding.ASCII.GetBytes(txt));
        String strpass = BitConverter.ToString(result);
        return strpass;
    }
    string key = "sieulienket.vn";
    public string Encrypt(string toEncrypt)
    {
        byte[] keyArray;
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        TripleDESCryptoServiceProvider tdes =
        new TripleDESCryptoServiceProvider();
        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = tdes.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(
        toEncryptArray, 0, toEncryptArray.Length);
        string strreturn = Convert.ToBase64String(resultArray, 0, resultArray.Length).Replace("/", "xet-").Replace("&", "va-").Replace("?", "hoi-").Replace("=", "ba-").Replace("+", "cong-");
        return strreturn;
    }

    public string Decrypt(string toDecrypt)
    {
        byte[] keyArray;
        byte[] toEncryptArray = Convert.FromBase64String(toDecrypt.Replace("xet-", "/").Replace("va-", "&").Replace("hoi-", "?").Replace("ba-", "=").Replace("cong-", "+"));

        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = tdes.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(
        toEncryptArray, 0, toEncryptArray.Length);
        return UTF8Encoding.UTF8.GetString(resultArray);
    }

    string javascripthash(string textpass)
    {
        //-1 mo phong javascript clien
        System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] bs = System.Text.Encoding.UTF8.GetBytes(textpass);
        bs = x.ComputeHash(bs);
        System.Text.StringBuilder s = new System.Text.StringBuilder();
        foreach (byte b in bs)
        {
            s.Append(b.ToString("x2").ToLower());
        }//-1
        return s.ToString();
    }
}