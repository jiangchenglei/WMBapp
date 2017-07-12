using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Bill.Utility.Security
{
    public class DES
    {
        //默认密钥向量
        private static byte[] Keys = { 0x09, 0xFE, 0xDC, 0xBA, 0x21, 0x87, 0x65, 0x43 };
        private static byte[] IVs = { 0x19, 0xCE, 0xFC, 0x0A, 0x31, 0x17, 0x22, 0x34 };
        private static string DESKey = "OES";

        /// <summary>
        /// DES 加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string Encrypt(string encryptString)
        {
            return Encrypt(encryptString, DESKey);
        }


        /// <summary>
        /// DES 解密
        /// </summary>
        /// <param name="decryptString"></param>
        /// <returns></returns>
        public static string Decrypt(string decryptString)
        {
            return Decrypt(decryptString, DESKey);
        }

        public static string Encrypt(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Keys;
                byte[] rgbIV = IVs;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }


        public static string Decrypt(string decryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Keys;
                byte[] rgbIV = IVs;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }


        #region 字符串编码
        /// <summary>
        /// 字符串编码
        /// </summary>
        /// <param name="str">需要编码的字符串</param>
        /// <returns></returns>
        public static string EncryptBase64(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                byte[] bytes = Encoding.Default.GetBytes(str);
                return Convert.ToBase64String(bytes);
            }
            else
            {
                return str;
            }
        }

        
        /// <summary>
        /// 字符串解码
        /// </summary>
        /// <param name="str">需要解码的字符串</param>
        /// <returns></returns>
        public static string DecryptBase64(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                byte[] oo = Convert.FromBase64String(str);
                return Encoding.Default.GetString(oo);
            }
            else
            {
                return str;
            }
        }
        #endregion
    }
}
