using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;

namespace Bill.Utility.Helper
{
    public static class ConvertHelper
    {
        static char[] code = new char[] {
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K',
        'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W',
        'X', 'Y'};
        /// <summary>
        /// 将INT转换成32进制的字符串
        /// </summary>
        /// <param name="number">INT数字, 负数会取绝对值</param>
        /// <returns></returns>
        public static string ToRadix32(int number)
        {
            return ToRadix32(Convert.ToUInt32(Math.Abs(number)));
        }
        /// <summary>
        /// 将UINT转换成32进制的字符串
        /// </summary>
        /// <param name="number">UINT数字</param>
        /// <returns></returns>
        public static string ToRadix32(uint number)
        {
            return ToRadix32((ulong)number);
        }
        /// <summary>
        /// 将LONG转换成32进制的字符串
        /// </summary>
        /// <param name="number">LONG数字, 负数会取绝对值</param>
        /// <returns></returns>
        public static string ToRadix32(long number)
        {
            return ToRadix32(Convert.ToUInt64(Math.Abs(number)));
        }
        /// <summary>
        /// 将ULONG转换成32进制的字符串
        /// </summary>
        /// <param name="number">ULONG数字</param>
        /// <returns></returns>
        public static string ToRadix32(ulong number)
        {
            string result = string.Empty;
            ulong temp = 0, numbase = 32;
            do
            {
                temp = (number % numbase);
                number = number / numbase;
                result = code[temp] + result;
            }
            while (number > 0);
            return result;
        }
        /// <summary>
        /// 将32进制的字符串转换成ULONG
        /// </summary>
        /// <param name="num32">32进制的字符串</param>
        /// <returns></returns>
        public static long FromRadix32(string num32)
        {
            long number = 0, temp = 0, numbase = 32, count = 0;
            foreach (var item in num32.Reverse())
            {
                temp = -1;
                for (int i = 0; i < numbase; i++)
                {
                    if (item == code[i])
                    {
                        temp = i;
                        break;
                    }
                }
                if (temp == -1)
                    break;

                number += (long)Math.Pow(numbase, count++) * temp;
            }
            return number;
        }

        public static double ToDouble(object obj)
        {
            return ToDouble(obj, 0.0D);
        }
        public static double ToDouble(object obj, double defaultValue)
        {
            if (null == obj)
            {
                return defaultValue;
            }
            try
            {
                return Convert.ToDouble(obj);
            }
            catch
            {
                return defaultValue;
            }
        }

     
        public static float ToFloat(object obj)
        {
            return ToFloat(obj, 0.0F);
        }
        public static float ToFloat(object obj, float defaultValue)
        {
            if (null == obj)
            {
                return defaultValue;
            }
            try
            {
                return Convert.ToSingle(obj);
            }
            catch
            {
                return defaultValue;
            }
        }
 
      

        public static T CopyBySerialize<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new System.ArgumentException("The type must be serializeble.", "source");
            }
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }
            IFormatter formatter = new BinaryFormatter();
            using (Stream theStream = new MemoryStream())
            {
                formatter.Serialize(theStream, source);
                theStream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(theStream);
            }
        }
        public static decimal ToDecimal(object obj)
        {
            return ToDecimal(obj, 0.0M);
        }
        public static decimal ToDecimal(object obj, decimal defaultValue)
        {
            if (null == obj)
            {
                return defaultValue;
            }
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static DateTime ToDateTime(object obj)
        {
            return ToDateTime(obj, new DateTime());
        }
        public static DateTime ToDateTime(object obj, DateTime defaultValue)
        {
            if (null == obj)
            {
                return defaultValue;
            }
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static Int32 ToInt32(object obj)
        {
            return ToInt32(obj, 0);
        }

        public static Int32 ToInt32(object obj, Int32 defaultValue)
        {
            if (null == obj)
            {
                return defaultValue;
            }
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static Int64 ToInt64(object obj)
        {
            return ToInt64(obj, 0L);
        }
        public static Int64 ToInt64(object obj, Int64 defaultValue)
        {
            if (null == obj)
            {
                return defaultValue;
            }
            try
            {
                return Convert.ToInt64(obj);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 按汇率进行货币转换
        /// </summary>
        /// <param name="srcCurrencyCode">原币代码，大写</param>
        /// <param name="srcExchangeRate">原币汇率</param>
        /// <param name="dstCurrencyCode"></param>
        /// <param name="dstExchangeRate"></param>
        /// <param name="srcAmount">原币金额</param>
        /// <returns></returns>
        public static decimal ToCurrency(string srcCurrencyCode, decimal srcExchangeRate
            , string dstCurrencyCode, decimal dstExchangeRate, decimal srcAmount)
        {
            decimal dstMoney = srcAmount;

            if (srcCurrencyCode != "USD")
            {
                if (srcExchangeRate > 0)
                {
                    dstMoney = dstMoney / srcExchangeRate;
                }
            }

            if (dstCurrencyCode != "USD")
            {
                if (dstExchangeRate > 0)
                {
                    dstMoney = dstMoney * dstExchangeRate;
                }
            }
            return dstMoney;
        }

        public static object ToDbValue(object obj)
        {
            if (null == obj)
            {
                return DBNull.Value;
            }
            return obj;
        }

        public static int ToModHashValue(object obj, int seed)
        {
            if (null == obj)
            {
                return 0;
            }
            if (seed == 0)
            {
                return 0;
            }
            int hash = obj.GetHashCode();
            return Math.Abs(hash % seed);

        }

        //public static string ToMD5String(string aString, Encoding anEncoding)
        //{
        //    MD5 m = new MD5CryptoServiceProvider();
        //    byte[] s = m.ComputeHash(anEncoding.GetBytes(aString));
        //    return BitConverter.ToString(s).Replace("-", "");
        //}
        /// <summary>
        /// 将HTML转换为纯文本，即去掉所有HTML标记。
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlToText(string html)
        {
            return Regex.Replace(html, "</?[a-z][a-z0-9]*[^<>]*>", String.Empty, RegexOptions.IgnoreCase);
        }


        public static string LeftString(string str, int length, string fix = "")
        {
            if (str.Length <= length)
                return str;
            else
                return str.Substring(0, length) + fix;
        }

        #region ToByte
        public static byte ToByte(object obj)
        {
            return ToByte(obj, 0);
        }

        public static byte ToByte(object obj, byte defaultvalue)
        {
            if (obj == null)
            {
                return defaultvalue;
            }
            try
            {
                return Convert.ToByte(obj);
            }
            catch
            {
                return defaultvalue;
            }
        }
        #endregion


        public static Byte ToByte(this string str)
        {
            if (str == null || str == "")
            {
                return 0;
            }
            Byte r = 0;
            try
            {
                bool b = Byte.TryParse(str, out r);
            }
            catch { }

            return r;
        }

        

        /// <summary>
        /// 时间转换为时间戳(秒)
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int DateTimeInt(DateTime? time)
        {
            if (!time.HasValue)
                time = DateTime.Now;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time.Value - startTime).TotalSeconds;
        }

        /// <summary>
        /// 时间转换为时间戳(毫秒)
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long DateTimeLong(DateTime? time)
        {
            if (!time.HasValue)
                time = DateTime.Now;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return ToInt64((time.Value - startTime).TotalMilliseconds);
        }


        public static DateTime longToDateTime(long timeu)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return startTime.AddMilliseconds(ConvertHelper.ToDouble(timeu));
        }

        #region 泛型转换
        /// <summary>
        /// 将指当前值转换为指定的类型<br/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToType<T>(object value)
        {
            Type t = typeof(T);
            //处理可空类型
            Type nullable = Nullable.GetUnderlyingType(t);
            if (nullable != null)
            {
                //如果传入的值是空引用或空字符串，返回默认值
                if (value == null || (value is string && string.IsNullOrEmpty(value as string)))
                {
                    return default(T);
                }
                t = nullable;
            }

            //转换
            object o = Convert.ChangeType(value, t.IsEnum ? Enum.GetUnderlyingType(t) : t);

            if (t.IsEnum && !HasFlagsAttribute(t) && !Enum.IsDefined(t, o)) throw new Exception("枚举类型\"" + t.ToString() + "\"中没有定义\"" + (o == null ? "null" : o.ToString()) + "\"");

            //可空类型
            if (nullable != null)
            {
                ConstructorInfo constructor = typeof(T).GetConstructor(new Type[] { nullable });
                return (T)constructor.Invoke(new object[] { o });
            }

            return (T)o;
        }

        /// <summary>
        /// 判断是否定义了FlagsAttribute属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool HasFlagsAttribute(Type type)
        {
            object[] attributes = type.GetCustomAttributes(typeof(FlagsAttribute), true);
            return attributes != null && attributes.Length > 0;
        }

        /// <summary>
        /// 将指当前值转换为指定的类型,如果转换失败返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T ToType<T>(object value, T defaultValue)
        {
            try
            {
                return ToType<T>(value);
            }
            catch
            {
                return defaultValue;
            }
        }


        /// <summary>
        /// 将序号转换为36进制两位编码
        /// </summary>
        /// <param name="intNum">序号</param>
        /// <returns>两位编码，如0F</returns>
        public static string ConvertToSuffixCode(int intNum)
        {
            string CodeList = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int intCodeLength = CodeList.Length; //进制
            if (intNum >= intCodeLength * intCodeLength)
            {
                throw new ApplicationException("喜好属性两位编码值超过最大长度");
            }

            List<int> lstIndex = new List<int>();
            while (true)
            {
                int m = intNum % intCodeLength;
                lstIndex.Add(m);
                intNum = intNum / intCodeLength;

                if (intNum < intCodeLength)
                {
                    lstIndex.Add(intNum);
                    break;
                }
            }

            List<char> lstChar = new List<char>();
            for (int i = lstIndex.Count - 1; i >= 0; i--)
            {
                lstChar.Add(CodeList[lstIndex[i]]);
            }

            string str = new string(lstChar.ToArray());
            return str;
        }
        #endregion
    }
}
