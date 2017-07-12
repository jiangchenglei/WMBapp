using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace Bill.Utility.Helper
{
    public static class StringExtend
    {
        public static string ObjToString(this object obj, string defaultval = null)
        {
            try
            {
                if (obj == null)
                {
                    return defaultval;
                }
                return obj.ToString();
            }
            catch (Exception)
            {
                return defaultval;
            }
        }

        /// <summary>
        /// 欲强之，故先弱之。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="part"></param>
        /// <param name="defaultval"></param>
        /// <returns></returns>
        public static string DecimalToString(this decimal? obj, string part, decimal defaultval = 0)
        {
            try
            {
                decimal Dobj = obj ?? defaultval;
                return Dobj.ToString(part);
            }
            catch (Exception)
            {
                return defaultval.ToString();
            }
        }


        public static string ObjReplace(this object obj, string oldstr, string newstr)
        {
            try
            {
                string res = obj.ObjToString();
                if (res != null)
                {
                    res = res.Replace(oldstr, newstr);
                }
                return res;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 失败返回-99999
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ObjToInt32(this object obj, int defaultval = -99999)
        {
            int result = defaultval;
            try
            {

                if (obj == null)
                {
                    return result;
                }

                Int32.TryParse(obj.ObjToString(), out result);

                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        public static long ObjToLong(this object obj, long defaultval = -99999)
        {
            long result = defaultval;
            try
            {

                if (obj == null)
                {
                    return result;
                }

                long.TryParse(obj.ObjToString(), out result);

                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        public static string DateToString(this DateTime? obj, string part)
        {
            try
            {
                if (obj == null)
                {
                    return null;
                }
                return Convert.ToDateTime(obj).ToString(part);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string HtmlToTxt(string strHtml)
        {
            if (string.IsNullOrEmpty(strHtml))
                return "";
            string[] aryReg ={
            @"<script[^>]*?>.*?</script>",
            @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
            @"([\r\n])[\s]+",
            @"&(quot|#34);",
            @"&(amp|#38);",
            @"&(lt|#60);",
            @"&(gt|#62);", 
            @"&(nbsp|#160);", 
            @"&(iexcl|#161);",
            @"&(cent|#162);",
            @"&(pound|#163);",
            @"&(copy|#169);",
            @"&#(\d+);",
            @"-->",
            @"<!--.*\n"
            };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, string.Empty);
            }
            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");
            return strOutput;
        }

        public static string ToStringEmpty(this object obj)
        {
            if (obj == null)
                return "";
            string s = obj.ToString();
            if (string.IsNullOrEmpty(s))
                return "";
            return s;
        }

        public static string ToJson(this object obj)
        {
            var result = new JavaScriptSerializer().Serialize(obj);
            //替换Json的Date字符串  
            string p = @"\\/Date\([^\(]{0,}\)\\/"; /*////Date/((([/+/-]/d+)|(/d+))[/+/-]/d+/)////*/

            MatchCollection matches = Regex.Matches(result, p);
            foreach (Match item in matches)
            {
                result = result.Replace(item.Value, _ConvertJsonDateToDateString(item.Value));
            }
            return result;
        }

        public static T Deserialize<T>(this string json)
        {
            return new JavaScriptSerializer().Deserialize<T>(json);
        }

        private static string _ConvertJsonDateToDateString(string date)
        {
            date = Regex.Match(date, "\\d{1,}").Value;
            if (string.IsNullOrWhiteSpace(date))
            {
                return date;
            }
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(date));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        /// <summary>
        /// like %xxx%完全匹配
        /// </summary>
        /// <param name="strQuery">匹配字符串</param>
        /// <returns></returns>
        public static string ReplaceSqlLike(this string strQuery)
        {
            string strRet = strQuery;
            strRet = strRet.Replace("/", "//");
            strRet = strRet.Replace("'", "''");
            strRet = strRet.Replace("%", "/%");
            strRet = strRet.Replace("[", "/[");
            strRet = "'%" + strRet + "%' escape '/'";
            return strRet;
        }

        /// <summary>
        /// like xxx%右匹配
        /// </summary>
        /// <param name="strQuery">匹配字符串</param>
        /// <returns></returns>
        public static string ReplaceQuickSqlLike(this string strQuery)
        {
            string strRet = strQuery;
            strRet = strRet.Replace("/", "//");
            strRet = strRet.Replace("'", "''");
            strRet = strRet.Replace("%", "/%");
            strRet = strRet.Replace("[", "/[");
            strRet = "'" + strRet + "%' escape '/'";
            return strRet;
        }
        /// <summary>
        /// 截取指定长度字符串
        /// </summary>
        /// <param name="input">入值</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string GetSubString(string input, int length)
        {
            string result = input;
            if (!string.IsNullOrEmpty(input))
            {
                result = result.Length > length ? result.Substring(0, length) : result;
            }
            return result;
        }

    }

    public static class ObjExtend
    {
        public static decimal ObjToDecimal(this object obj)
        {
            try
            {
                if (obj == null)
                {
                    return 0;
                }
                return Convert.ToDecimal(obj);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static DateTime ObjToDateTime(this object obj)
        {
            try
            {
                if (obj == null)
                {
                    return new DateTime();
                }
                return DateTime.Parse(obj.ObjToString("1900-01-01"));
            }
            catch (Exception)
            {
                return new DateTime();
                
            }
        }

    }
}
