// Decompiled with JetBrains decompiler
// Type: m2ostnext.Models.AESAlgorithm
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace m2ostnext.Models
{
  internal class AESAlgorithm
  {
    public string Encrypt(string key, string data)
    {
      string str = (string) null;
      byte[][] hashKeys = this.GetHashKeys(key);
      try
      {
        str = AESAlgorithm.EncryptStringToBytes_Aes(data, hashKeys[0], hashKeys[1]);
      }
      catch (CryptographicException ex)
      {
      }
      catch (ArgumentNullException ex)
      {
      }
      return str;
    }

    public string Decrypt(string key, string data)
    {
      string str = (string) null;
      byte[][] hashKeys = this.GetHashKeys(key);
      try
      {
        str = AESAlgorithm.DecryptStringFromBytes_Aes(data, hashKeys[0], hashKeys[1]);
      }
      catch (CryptographicException ex)
      {
      }
      catch (ArgumentNullException ex)
      {
      }
      return str;
    }

    private byte[][] GetHashKeys(string key)
    {
      byte[][] hashKeys = new byte[2][];
      Encoding utF8 = Encoding.UTF8;
      SHA256 shA256 = (SHA256) new SHA256CryptoServiceProvider();
      byte[] bytes1 = utF8.GetBytes(key);
      byte[] bytes2 = utF8.GetBytes(key);
      byte[] hash1 = shA256.ComputeHash(bytes1);
      byte[] hash2 = shA256.ComputeHash(bytes2);
      Array.Resize<byte>(ref hash2, 16);
      hashKeys[0] = hash1;
      hashKeys[1] = hash2;
      return hashKeys;
    }

    private static string EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
    {
      if (plainText == null || plainText.Length <= 0)
        throw new ArgumentNullException(nameof (plainText));
      if (Key == null || Key.Length == 0)
        throw new ArgumentNullException(nameof (Key));
      if (IV == null || IV.Length == 0)
        throw new ArgumentNullException(nameof (IV));
      byte[] array;
      using (AesManaged aesManaged = new AesManaged())
      {
        aesManaged.Key = Key;
        aesManaged.IV = IV;
        ICryptoTransform encryptor = aesManaged.CreateEncryptor(aesManaged.Key, aesManaged.IV);
        using (MemoryStream memoryStream = new MemoryStream())
        {
          using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write))
          {
            using (StreamWriter streamWriter = new StreamWriter((Stream) cryptoStream))
              streamWriter.Write(plainText);
            array = memoryStream.ToArray();
          }
        }
      }
      return Convert.ToBase64String(array);
    }

    private static string DecryptStringFromBytes_Aes(
      string cipherTextString,
      byte[] Key,
      byte[] IV)
    {
      byte[] buffer = Convert.FromBase64String(cipherTextString);
      if (buffer == null || buffer.Length == 0)
        throw new ArgumentNullException("cipherText");
      if (Key == null || Key.Length == 0)
        throw new ArgumentNullException(nameof (Key));
      if (IV == null || IV.Length == 0)
        throw new ArgumentNullException(nameof (IV));
      using (Aes aes = Aes.Create())
      {
        aes.Key = Key;
        aes.IV = IV;
        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using (MemoryStream memoryStream = new MemoryStream(buffer))
        {
          using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, decryptor, CryptoStreamMode.Read))
          {
            using (StreamReader streamReader = new StreamReader((Stream) cryptoStream))
              return streamReader.ReadToEnd();
          }
        }
      }
    }

    public string EncryptString(string plainText, byte[] key, byte[] iv)
    {
      Aes aes = Aes.Create();
      aes.Mode = CipherMode.CBC;
      aes.Key = key;
      aes.IV = iv;
      MemoryStream memoryStream = new MemoryStream();
      ICryptoTransform encryptor = aes.CreateEncryptor();
      CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write);
      byte[] bytes = Encoding.ASCII.GetBytes(plainText);
      cryptoStream.Write(bytes, 0, bytes.Length);
      cryptoStream.FlushFinalBlock();
      byte[] array = memoryStream.ToArray();
      memoryStream.Close();
      cryptoStream.Close();
      return Convert.ToBase64String(array, 0, array.Length);
    }

    public string DecryptString(string cipherText, byte[] key, byte[] iv)
    {
      Aes aes = Aes.Create();
      aes.Mode = CipherMode.CBC;
      aes.Key = key;
      aes.IV = iv;
      MemoryStream memoryStream = new MemoryStream();
      ICryptoTransform decryptor = aes.CreateDecryptor();
      CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, decryptor, CryptoStreamMode.Write);
      string empty = string.Empty;
      try
      {
        byte[] buffer = Convert.FromBase64String(cipherText);
        cryptoStream.Write(buffer, 0, buffer.Length);
        cryptoStream.FlushFinalBlock();
        byte[] array = memoryStream.ToArray();
        return Encoding.ASCII.GetString(array, 0, array.Length);
      }
      finally
      {
        memoryStream.Close();
        cryptoStream.Close();
      }
    }

    public string getEncryptedString(string str)
    {
      string s = "3sc3RLrpd17";
      byte[] hash = SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(s));
      byte[] iv = new byte[16];
      return new AESAlgorithm().EncryptString(str, hash, iv);
    }

    public string getDecryptedString(string str)
    {
      string s = "3sc3RLrpd17";
      byte[] hash = SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(s));
      byte[] iv = new byte[16];
      return new AESAlgorithm().DecryptString(str, hash, iv);
    }
  }
}
