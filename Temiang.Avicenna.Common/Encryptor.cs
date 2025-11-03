using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Temiang.Avicenna.Common
{
    // Encryption & Decryption using DES algorithm
    public class Encryptor
    {
        public static string Encrypt(string strData)
        {
            return Encrypt(strData, "versi", "dua");
        }

        public static string Decrypt(string strData)
        {
            return Decrypt(strData, "versi", "dua");
        }

        //Encrypt function
        public static string Encrypt(string strData, string strKey1, string strKey2)
        {
            var ms = new MemoryStream();

            var objKey = new DESCryptoServiceProvider {Key = objLockKey(strKey1), IV = objLockKey(strKey2)};

            var encStream = new CryptoStream(ms,
            objKey.CreateEncryptor(), CryptoStreamMode.Write);
            var sw = new StreamWriter(encStream);
            sw.WriteLine(strData);
            sw.Close();
            encStream.Close();

            var bytData = ms.ToArray();
            var strReturnData = bytData.Aggregate("", (current, bytChar) => current + bytChar.ToString().PadLeft(3, Convert.ToChar("0")));

            ms.Close();

            return strReturnData;
        }

        //Decrypt function
        public static string Decrypt(string strData, string strKey1, string strKey2)
        {

            var objKey = new DESCryptoServiceProvider();
            objKey.Key = objLockKey(strKey1);
            objKey.IV = objLockKey(strKey2);

            var intLength = Convert.ToInt16((strData.Length / 3));
            var bytData = new byte[intLength];

            for (var intCount = 0; intCount < intLength; intCount++)
            {
                var strChar = strData.Substring((intCount * 3), 3);
                bytData[intCount] = Convert.ToByte(strChar);
            }

            var ms = new MemoryStream(bytData);

            var encStream = new CryptoStream(ms,
            objKey.CreateDecryptor(), CryptoStreamMode.Read);
            var sr = new StreamReader(encStream);
            var strReturnVal = sr.ReadLine();
            sr.Close();
            encStream.Close();
            ms.Close();

            return strReturnVal;
        }

        private static byte[] objLockKey(string strPassword)
        {
            const int intKeyLength = 8;
            strPassword = strPassword.PadRight(intKeyLength,
            Convert.ToChar(".")).Substring(0, intKeyLength);
            var objKey = new byte[strPassword.Length];
            for (var intCount = 0; intCount < strPassword.Length; intCount++)
            {
                objKey[intCount] = Convert.ToByte(Convert.ToChar(strPassword.Substring(intCount, 1)));
            }
            return objKey;
        }
    }
}