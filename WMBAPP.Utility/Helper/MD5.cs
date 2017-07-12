using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMBAPP.Utility.Security
{
    public class MD5
    {
        public static string Encrypt(string data)
        {
            byte[] b = System.Text.Encoding.UTF8.GetBytes(data);
            return Encrypt(b);
        }
        public static string Encrypt(byte[] data)
        {
            data = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(data);
            string ret = string.Empty;
            for (int i = 0; i < data.Length; i++)
            {
                ret += data[i].ToString("X2");
            }
            return ret;
        }
    }
}
