namespace Auth.GG;

internal class Encryption
{
    internal static string APIService(string value)
    {
        var password = Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTKEY));
        var mySHA256 = SHA256.Create();
        var key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));
        var iv = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTSALT)));
        var encrypted = EncryptString(value, key, iv);
        return encrypted;
    }

    internal static string EncryptService(string value)
    {
        var password = Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTKEY));
        var mySHA256 = SHA256.Create();
        var key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));
        var iv = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTSALT)));
        var encrypted = EncryptString(value, key, iv);
        var property = int.Parse((AuthGG.AID.Substring(0, 2)));
        var final = encrypted + Security.Obfuscate(property);
        return final;
    }

    internal static string DecryptService(string value)
    {
        var password = Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTKEY));
        var mySHA256 = SHA256.Create();
        var key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));
        var iv = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTSALT)));
        var decrypted = DecryptString(value, key, iv);
        return decrypted;
    }

    internal static string EncryptString(string plainText, byte[] key, byte[] iv)
    {
        var encryptor = Aes.Create();
        encryptor.Mode = CipherMode.CBC;
        encryptor.Key = key;
        encryptor.IV = iv;
        var memoryStream = new MemoryStream();
        var aesEncryptor = encryptor.CreateEncryptor();
        var cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);
        var plainBytes = Encoding.ASCII.GetBytes(plainText);
        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
        cryptoStream.FlushFinalBlock();
        var cipherBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        var cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
        return cipherText;
    }

    internal static string DecryptString(string cipherText, byte[] key, byte[] iv)
    {
        var encryptor = Aes.Create();
        encryptor.Mode = CipherMode.CBC;
        encryptor.Key = key;
        encryptor.IV = iv;
        var memoryStream = new MemoryStream();
        var aesDecryptor = encryptor.CreateDecryptor();
        var cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);
        string plainText;
        try
        {
            var cipherBytes = Convert.FromBase64String(cipherText);
            cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
            cryptoStream.FlushFinalBlock();
            var plainBytes = memoryStream.ToArray();
            plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
        }
        finally
        {
            memoryStream.Close();
            cryptoStream.Close();
        }
        return plainText;
    }

    internal static string Decode(string text)
    {
        text = text.Replace('_', '/').Replace('-', '+');
        switch (text.Length % 4)
        {
            case 2:
                text += "==";
                break;

            case 3:
                text += "=";
                break;
        }
        return Encoding.UTF8.GetString(Convert.FromBase64String(text));
    }
}