using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WMB.Utility.Helper
{
    public class ReqUtils
    {
        /// <summary> 
        /// 返回HttpContext.Current.Request对象 
        /// </summary> 
        public static HttpRequest Request
        {
            get { return HttpContext.Current.Request; }
        }

        /// <summary> 
        /// 从Form获取字符串类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static string FormString(string name)
        {
            return Request.Form[name];
        }
        /// <summary> 
        /// 从Form获取字符串类型的参数,如果没有则返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static string FormString(string name, string defaultValue)
        {
            string value = Request.Form[name];
            return value == null ? defaultValue : value;
        }
        /// <summary> 
        /// 从Query获取字符串类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static string QueryString(string name)
        {
            return Request.QueryString[name];
        }
        /// <summary> 
        /// 从Query获取字符串类型的参数,如果没有则返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static string QueryString(string name, string defaultValue)
        {
            string value = Request.QueryString[name];
            return value == null ? defaultValue : value;
        }
        /// <summary> 
        /// 先从Query获取参数，如果没有再从Form获取 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static string GetString(string name)
        {
            /* 
            string value = Request.QueryString[name]; 
            if (value == null) 
            { 
                value = Request.Form[name]; 
            } 
            return value;*/
            return Param(name);
        }

        public static string Param(string name)
        {
            return Request.Params[name];
        }

        public static string[] GetValues(string name)
        {
            string[] values = Request.Params.GetValues(name);
            return values == null ? new string[0] : values;
        }

        /// <summary> 
        /// 先从Form获取参数，如果没有再从Query获取,如果仍然为空，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static string GetString(string name, string defaultValue)
        {
            string value = GetString(name);
            return value == null ? defaultValue : value;
        }
        /// <summary> 
        /// 从Form获取参数的字符串数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static string[] FormStringArray(string name)
        {
            string[] values = Request.Form.GetValues(name);
            return values == null ? new string[0] : values;
        }
        /// <summary> 
        /// 从Query获取参数的字符串数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static string[] QueryStringArray(string name)
        {
            string[] values = Request.QueryString.GetValues(name);
            return values == null ? new string[0] : values;
        }
        /// <summary> 
        /// 从Form和Query获取参数的字符串数组，返回两个地方的总和 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static string[] GetStringArray(string name)
        {
            string[] formArray = FormStringArray(name);
            string[] queryArray = QueryStringArray(name);
            string[] strArray = new string[formArray.Length + queryArray.Length];
            Array.ConstrainedCopy(formArray, 0, strArray, 0, formArray.Length);
            Array.ConstrainedCopy(queryArray, 0, strArray, formArray.Length, queryArray.Length);
            return strArray;
        }

        /*================Boolean类型参数获取================*/
        /// <summary> 
        /// 从Form获取bool类型的参数 支持"True" "true" "False" "false" 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static bool FormBool(string name)
        {
            string str = FormString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            if ("True" != str && "true" != str && "1" != str && "False" != str && "false" != str && "0" != str) throw new Exception("错误的Boolean表达式！");
            return "True" == str || "true" == str || "1" == str;
        }
        /// <summary> 
        /// 从Form获取bool类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static bool FormBool(string name, bool defaultValue)
        {
            try
            {
                return FormBool(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form获取bool类型的参数 支持"True" "true" "False" "false" 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static bool? FormNullableBool(string name)
        {
            string str = FormString(name);
            if (string.IsNullOrEmpty(str)) return null;
            if ("True" != str && "true" != str && "1" != str && "False" != str && "false" != str && "0" != str) throw new Exception("错误的Boolean表达式！");
            return "True" == str || "true" == str || "1" == str;
        }
        /// <summary> 
        /// 从Form获取bool类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static bool? FormNullableBool(string name, bool? defaultValue)
        {
            try
            {
                return FormNullableBool(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Query获取int类型的参数 支持"True" "true" "False" "false" 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static bool QueryBool(string name)
        {
            string str = QueryString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            if ("True" != str && "true" != str && "1" != str && "False" != str && "false" != str && "0" != str) throw new Exception("错误的Boolean表达式！");
            return "True" == str || "true" == str || "1" == str;
        }
        /// <summary> 
        /// 从Query获取bool类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static bool QueryBool(string name, bool defaultValue)
        {
            try
            {
                return QueryBool(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Query获取int类型的参数 支持"True" "true" "False" "false" 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static bool? QueryNullableBool(string name)
        {
            string str = QueryString(name);
            if (string.IsNullOrEmpty(str)) return null;
            if ("True" != str && "true" != str && "1" != str && "False" != str && "false" != str && "0" != str) throw new Exception("错误的Boolean表达式！");
            return "True" == str || "true" == str || "1" == str;
        }
        /// <summary> 
        /// 从Query获取bool类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static bool? QueryNullableBool(string name, bool? defaultValue)
        {
            try
            {
                return QueryNullableBool(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form获取bool参数的数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static bool[] FormBoolArray(string name)
        {
            string[] strArray = FormStringArray(name);
            return Array.ConvertAll<string, bool>(strArray, delegate(string str)
            {
                if ("True" != str && "true" != str && "1" != str && "False" != str && "false" != str && "0" != str) throw new Exception("错误的Boolean表达式！");
                return "True" == str || "true" == str || "1" == str;
            });
        }
        /// <summary> 
        /// 从Query获取bool参数的数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static bool[] QueryBoolArray(string name)
        {
            string[] strArray = QueryStringArray(name);
            return Array.ConvertAll<string, bool>(strArray, delegate(string str)
            {
                if ("True" != str && "true" != str && "1" != str && "False" != str && "false" != str && "0" != str) throw new Exception("错误的Boolean表达式！");
                return "True" == str || "true" == str || "1" == str;
            });
        }

        /// <summary> 
        /// 从Form和Query获取bool类型的参数 支持"True" "true" "False" "false" 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static bool GetBool(string name)
        {
            string str = GetString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            if ("True" != str && "true" != str && "1" != str && "False" != str && "false" != str && "0" != str) throw new Exception("错误的Boolean表达式！");
            return "True" == str || "true" == str || "1" == str;
        }
        /// <summary> 
        /// 从Form和Query获取bool类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static bool GetBool(string name, bool defaultValue)
        {
            try
            {
                return GetBool(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form和Query获取bool类型的参数 支持"True" "true" "False" "false" 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static bool? GetNullableBool(string name)
        {
            string str = GetString(name);
            if (string.IsNullOrEmpty(str)) return null;
            if ("True" != str && "true" != str && "1" != str && "False" != str && "false" != str && "0" != str) throw new Exception("错误的Boolean表达式！");
            return "True" == str || "true" == str || "1" == str;
        }
        /// <summary> 
        /// 从Form和Query获取bool类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static bool? GetNullableBool(string name, bool? defaultValue)
        {
            try
            {
                return GetNullableBool(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /*================Int类型参数获取================*/
        /// <summary> 
        /// 从Form获取int类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static int FormInt(string name)
        {
            string str = FormString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            return Convert.ToInt32(str);
        }
        /// <summary> 
        /// 从Form获取int类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static int FormInt(string name, int defaultValue)
        {
            try
            {
                return FormInt(name);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary> 
        /// 从Form获取int类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static int? FormNullableInt(string name)
        {
            string str = FormString(name);
            if (string.IsNullOrEmpty(str)) return null;
            return Convert.ToInt32(str);
        }
        /// <summary> 
        /// 从Form获取int类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static int? FormNullableInt(string name, int? defaultValue)
        {
            try
            {
                return FormNullableInt(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Query获取int类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static int QueryInt(string name)
        {
            string str = QueryString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            return Convert.ToInt32(str);
        }
        /// <summary> 
        /// 从Query获取int类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static int QueryInt(string name, int defaultValue)
        {
            try
            {
                return QueryInt(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Query获取int类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static int? QueryNullableInt(string name)
        {
            string str = QueryString(name);
            if (string.IsNullOrEmpty(str)) return null;
            return Convert.ToInt32(str);
        }
        /// <summary> 
        /// 从Query获取int类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static int? QueryNullableInt(string name, int? defaultValue)
        {
            try
            {
                return QueryNullableInt(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form获取int参数的数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static int[] FormIntArray(string name)
        {
            string[] strArray = FormStringArray(name);
            return Array.ConvertAll<string, int>(strArray, delegate(string str)
            {
                return Convert.ToInt32(str);
            });
        }
        /// <summary> 
        /// 从Query获取int参数的数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static int[] QueryIntArray(string name)
        {
            string[] strArray = QueryStringArray(name);
            return Array.ConvertAll<string, int>(strArray, delegate(string str)
            {
                return Convert.ToInt32(str);
            });
        }

        /// <summary> 
        /// 从query和form获取int数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static int[] GetIntArray(string name)
        {
            string[] strArray = GetValues(name);
            if (strArray == null) return new int[0];
            return Array.ConvertAll<string, int>(strArray, delegate(string str)
            {
                return Convert.ToInt32(str);
            });
        }

        /// <summary> 
        /// 从Form和Query获取int类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static int GetInt(string name)
        {
            string str = GetString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            return Convert.ToInt32(str);
        }
        /// <summary> 
        /// 从Form和Query获取int类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static int GetInt(string name, int defaultValue)
        {
            try
            {
                return GetInt(name);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary> 
        /// 从Form和Query获取int类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static int? GetNullableInt(string name)
        {
            string str = GetString(name);
            if (string.IsNullOrEmpty(str)) return null;
            return Convert.ToInt32(str);
        }
        /// <summary> 
        /// 从Form和Query获取int类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static int? GetNullableInt(string name, int? defaultValue)
        {
            try
            {
                return GetNullableInt(name);
            }
            catch
            {
                return defaultValue;
            }
        }

        /*================Long类型参数获取================*/
        /// <summary> 
        /// 从Form获取long 类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static long FormLong(string name)
        {
            string str = FormString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            return Convert.ToInt64(str);
        }
        /// <summary> 
        /// 从Form获取long 类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static long FormLong(string name, long defaultValue)
        {
            try
            {
                return FormLong(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form获取long 类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static long? FormNullableLong(string name)
        {
            string str = FormString(name);
            if (string.IsNullOrEmpty(str)) return null;
            return Convert.ToInt64(str);
        }
        /// <summary> 
        /// 从Form获取long 类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static long? FormNullableLong(string name, long? defaultValue)
        {
            try
            {
                return FormNullableLong(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Query获取long 类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static long QueryLong(string name)
        {
            string str = QueryString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            return Convert.ToInt64(str);
        }
        /// <summary> 
        /// 从Query获取long 类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static long QueryLong(string name, long defaultValue)
        {
            try
            {
                return QueryLong(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Query获取long 类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static long? QueryNullableLong(string name)
        {
            string str = QueryString(name);
            if (string.IsNullOrEmpty(str)) return null;
            return Convert.ToInt64(str);
        }
        /// <summary> 
        /// 从Query获取long 类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static long? QueryNullableLong(string name, long? defaultValue)
        {
            try
            {
                return QueryNullableLong(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form获取long类型参数数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static long[] FormLongArray(string name)
        {
            string[] strArray = FormStringArray(name);
            return Array.ConvertAll<string, long>(strArray, delegate(string str)
            {
                return Convert.ToInt64(str);
            });
        }
        /// <summary> 
        /// 从Query获取long类型参数数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static long[] QueryLongArray(string name)
        {
            string[] strArray = QueryStringArray(name);
            return Array.ConvertAll<string, long>(strArray, delegate(string str)
            {
                return Convert.ToInt64(str);
            });
        }

        /// <summary> 
        /// 从query和form获取long数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static long[] GetLongArray(string name)
        {
            string[] strArray = GetValues(name);
            return Array.ConvertAll<string, long>(strArray, delegate(string str)
            {
                return Convert.ToInt64(str);
            });
        }
        /// <summary> 
        /// 从Form和Query获取long 类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static long GetLong(string name)
        {
            string str = GetString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            return Convert.ToInt64(str);
        }
        /// <summary> 
        /// 从Form和Query获取long 类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static long GetLong(string name, long defaultValue)
        {
            try
            {
                return GetLong(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form和Query获取long 类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static long? GetNullableLong(string name)
        {
            string str = GetString(name);
            if (string.IsNullOrEmpty(str)) return null;
            return Convert.ToInt64(str);
        }
        /// <summary> 
        /// 从Form和Query获取long 类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static long? GetNullableLong(string name, long? defaultValue)
        {
            try
            {
                return GetNullableLong(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /*================Float类型参数获取================*/
        /// <summary> 
        /// 从Form获取float类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static float FormFloat(string name)
        {
            string str = FormString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            return float.Parse(str);
        }
        /// <summary> 
        /// 从Form获取float类型的参数，如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static float FormFloat(string name, float defaultValue)
        {
            try
            {
                return FormFloat(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form获取float类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static float? FormNullableFloat(string name)
        {
            string str = FormString(name);
            if (string.IsNullOrEmpty(str)) return null;
            return float.Parse(str);
        }
        /// <summary> 
        /// 从Form获取float类型的参数，如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static float? FormNullableFloat(string name, float? defaultValue)
        {
            try
            {
                return FormNullableFloat(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Query获取float类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static float QueryFloat(string name)
        {
            string str = QueryString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            return float.Parse(str);
        }
        /// <summary> 
        /// 从Query获取float类型的参数，如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static float QueryFloat(string name, float defaultValue)
        {
            try
            {
                return QueryFloat(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Query获取float类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static float? QueryNullableFloat(string name)
        {
            string str = QueryString(name);
            if (string.IsNullOrEmpty(str)) return null;
            return float.Parse(str);
        }
        /// <summary> 
        /// 从Query获取float类型的参数，如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static float? QueryNullableFloat(string name, float? defaultValue)
        {
            try
            {
                return QueryNullableFloat(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form获取float参数数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static float[] FormFloatArray(string name)
        {
            string[] strArray = FormStringArray(name);
            return Array.ConvertAll<string, float>(strArray, delegate(string str)
            {
                return float.Parse(str);
            });
        }
        /// <summary> 
        /// 从Query获取float参数数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static float[] QueryFloatArray(string name)
        {
            string[] strArray = QueryStringArray(name);
            return Array.ConvertAll<string, float>(strArray, delegate(string str)
            {
                return float.Parse(str);
            });
        }

        /// <summary> 
        /// 从Form和Query获取float类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static float GetFloat(string name)
        {
            string str = GetString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            return float.Parse(str);
        }
        /// <summary> 
        /// 从Form和Query获取float类型的参数，如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static float GetFloat(string name, float defaultValue)
        {
            try
            {
                return GetFloat(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form和Query获取float类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static float? GetNullableFloat(string name)
        {
            string str = GetString(name);
            if (string.IsNullOrEmpty(str)) return null;
            return float.Parse(str);
        }
        /// <summary> 
        /// 从Form和Query获取float类型的参数，如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static float? GetNullableFloat(string name, float? defaultValue)
        {
            try
            {
                return GetNullableFloat(name);
            }
            catch
            {
                return defaultValue;
            }
        }

        /*================Double类型参数获取================*/
        /// <summary> 
        /// 从Form获取double类型参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static double FormDouble(string name)
        {
            string str = FormString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            return double.Parse(str);
        }
        /// <summary> 
        /// 从Form获取double类型参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static double FormDouble(string name, double defaultValue)
        {
            try
            {
                return FormDouble(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form获取double类型参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static double? FormNullableDouble(string name)
        {
            string str = FormString(name);
            if (string.IsNullOrEmpty(str)) return null;
            return double.Parse(str);
        }
        /// <summary> 
        /// 从Form获取double类型参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static double? FormNullableDouble(string name, double? defaultValue)
        {
            try
            {
                return FormNullableDouble(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Query获取double类型参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static double QueryDouble(string name)
        {
            string str = QueryString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            return double.Parse(str);
        }
        /// <summary> 
        /// 从Query获取double类型参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static double QueryDouble(string name, double defaultValue)
        {
            try
            {
                return QueryDouble(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Query获取double类型参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static double? QueryNullableDouble(string name)
        {
            string str = QueryString(name);
            if (string.IsNullOrEmpty(str)) return null;
            return double.Parse(str);
        }
        /// <summary> 
        /// 从Query获取double类型参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static double? QueryNullableDouble(string name, double? defaultValue)
        {
            try
            {
                return QueryNullableDouble(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form获取double类型参数的数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static double[] FormDoubleArray(string name)
        {
            string[] strArray = FormStringArray(name);
            return Array.ConvertAll<string, double>(strArray, delegate(string str)
            {
                return double.Parse(str);
            });
        }
        /// <summary> 
        /// 从Query获取double类型的参数数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static double[] QueryDoubleArray(string name)
        {
            string[] strArray = QueryStringArray(name);
            return Array.ConvertAll<string, double>(strArray, delegate(string str)
            {
                return double.Parse(str);
            });
        }

        /// <summary> 
        /// 从Form和Query获取double类型参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static double GetDouble(string name)
        {
            string str = GetString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            return double.Parse(str);
        }
        /// <summary> 
        /// 从Form和Query获取double类型参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static double GetDouble(string name, double defaultValue)
        {
            try
            {
                return GetDouble(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form和Query获取double类型参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static double? GetNullableDouble(string name)
        {
            string str = GetString(name);
            if (string.IsNullOrEmpty(str)) return null;
            return double.Parse(str);
        }
        /// <summary> 
        /// 从Form和Query获取double类型参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static double? GetNullableDouble(string name, double? defaultValue)
        {
            try
            {
                return GetNullableDouble(name);
            }
            catch
            {
                return defaultValue;
            }
        }

        /*================Decimal类型============================*/
        /// <summary> 
        /// 从Form获取decimal类型参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static decimal FormDecimal(string name)
        {
            string str = FormString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            return decimal.Parse(str);
        }
        /// <summary> 
        /// 从Form获取decimal类型参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static decimal FormDecimal(string name, decimal defaultValue)
        {
            try
            {
                return FormDecimal(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form获取decimal类型参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static decimal? FormNullableDecimal(string name)
        {
            string str = FormString(name);
            if (string.IsNullOrEmpty(str)) return null;
            return decimal.Parse(str);
        }
        /// <summary> 
        /// 从Form获取decimal类型参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static decimal? FormNullableDecimal(string name, decimal? defaultValue)
        {
            try
            {
                return FormNullableDecimal(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Query获取decimal类型参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static decimal QueryDecimal(string name)
        {
            string str = QueryString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            return decimal.Parse(str);
        }
        /// <summary> 
        /// 从Query获取decimal类型参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static decimal QueryDecimal(string name, decimal defaultValue)
        {
            try
            {
                return QueryDecimal(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Query获取decimal类型参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static decimal? QueryNullableDecimal(string name)
        {
            string str = QueryString(name);
            if (string.IsNullOrEmpty(str)) return null;
            return decimal.Parse(str);
        }
        /// <summary> 
        /// 从Query获取decimal类型参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static decimal? QueryNullableDecimal(string name, decimal? defaultValue)
        {
            try
            {
                return QueryNullableDecimal(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form获取decimal类型参数的数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static decimal[] FormDecimalArray(string name)
        {
            string[] strArray = FormStringArray(name);
            return Array.ConvertAll<string, decimal>(strArray, delegate(string str)
            {
                return decimal.Parse(str);
            });
        }
        /// <summary> 
        /// 从Query获取decimal类型的参数数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static decimal[] QueryDecimalArray(string name)
        {
            string[] strArray = QueryStringArray(name);
            return Array.ConvertAll<string, decimal>(strArray, delegate(string str)
            {
                return decimal.Parse(str);
            });
        }

        /// <summary> 
        /// 从Form和Query获取decimal类型参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static decimal GetDecimal(string name)
        {
            string str = GetString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            return decimal.Parse(str);
        }
        /// <summary> 
        /// 从Form和Query获取decimal类型参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static decimal GetDecimal(string name, decimal defaultValue)
        {
            try
            {
                return GetDecimal(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form和Query获取decimal类型参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static decimal? GetNullableDecimal(string name)
        {
            string str = GetString(name);
            if (string.IsNullOrEmpty(str)) return null;
            return decimal.Parse(str);
        }
        /// <summary> 
        /// 从Form和Query获取decimal类型参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static decimal? GetNullableDecimal(string name, decimal? defaultValue)
        {
            try
            {
                return GetNullableDecimal(name);
            }
            catch
            {
                return defaultValue;
            }
        }


        /*================DateTime类型参数获取================*/
        /// <summary> 
        /// 从Form获取DateTime类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static DateTime FormDateTime(string name)
        {
            string str = FormString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            return DateTime.Parse(str);
        }
        /// <summary> 
        /// 从Form获取DateTime类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static DateTime FormDateTime(string name, DateTime defaultValue)
        {
            try
            {
                return FormDateTime(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form获取DateTime类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static DateTime? FormNullableDateTime(string name)
        {
            string str = FormString(name);
            if (string.IsNullOrEmpty(str)) return null;
            return DateTime.Parse(str);
        }
        /// <summary> 
        /// 从Form获取DateTime类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static DateTime? FormNullableDateTime(string name, DateTime? defaultValue)
        {
            try
            {
                return FormNullableDateTime(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Query获取DateTime类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static DateTime QueryDateTime(string name)
        {
            string str = QueryString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            return DateTime.Parse(str);
        }
        /// <summary> 
        /// 从Query获取DateTime类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static DateTime QueryDateTime(string name, DateTime defaultValue)
        {
            try
            {
                return QueryDateTime(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Query获取DateTime类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static DateTime? QueryNullableDateTime(string name)
        {
            string str = QueryString(name);
            if (string.IsNullOrEmpty(str)) return null;
            return DateTime.Parse(str);
        }
        /// <summary> 
        /// 从Query获取DateTime类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static DateTime? QueryNullableDateTime(string name, DateTime? defaultValue)
        {
            try
            {
                return QueryNullableDateTime(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form获取DateTime类型参数数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static DateTime[] FormDateTimeArray(string name)
        {
            string[] strArray = FormStringArray(name);
            return Array.ConvertAll<string, DateTime>(strArray, delegate(string str)
            {
                return DateTime.Parse(str);
            });
        }
        /// <summary> 
        /// 从Query获取DateTime类型参数数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static DateTime[] QueryDateTimeArray(string name)
        {
            string[] strArray = QueryStringArray(name);
            return Array.ConvertAll<string, DateTime>(strArray, delegate(string str)
            {
                return DateTime.Parse(str);
            });
        }

        /// <summary> 
        /// 从Form获取DateTime?类型的参数数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static DateTime?[] FormNullableDateTimeArray(string name)
        {
            string[] strArray = FormStringArray(name);
            return Array.ConvertAll<string, DateTime?>(strArray, delegate(string str)
            {
                if (string.IsNullOrEmpty(str)) return null;
                return DateTime.Parse(str);
            });
        }
        /// <summary> 
        /// 从Query获取DateTime?类型的参数数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static DateTime?[] QueryNullableDateTimeArray(string name)
        {
            string[] strArray = QueryStringArray(name);
            return Array.ConvertAll<string, DateTime?>(strArray, delegate(string str)
            {
                if (string.IsNullOrEmpty(str)) return null;
                return DateTime.Parse(str);
            });
        }


        /// <summary> 
        /// 从Form和Query获取DateTime类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static DateTime GetDateTime(string name)
        {
            string str = GetString(name);
            if (string.IsNullOrEmpty(str)) throw new Exception("没有名为“" + name + "”的参数！");
            return DateTime.Parse(str);
        }
        /// <summary> 
        /// 从Form和Query获取DateTime类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static DateTime GetDateTime(string name, DateTime defaultValue)
        {
            try
            {
                return GetDateTime(name);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary> 
        /// 从Form和Query获取DateTime类型的参数 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static DateTime? GetNullableDateTime(string name)
        {
            string str = GetString(name);
            if (string.IsNullOrEmpty(str)) return null;
            return DateTime.Parse(str);
        }
        /// <summary> 
        /// 从Form和Query获取DateTime类型的参数,如果获取或转换失败，返回默认值 
        /// </summary> 
        /// <param name="name"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public static DateTime? GetNullableDateTime(string name, DateTime? defaultValue)
        {
            try
            {
                return GetNullableDateTime(name);
            }
            catch
            {
                return defaultValue;
            }
        }

        /*================HttpPostedFile类型参数获取================*/
        /// <summary> 
        /// 获取指定参数名的文件 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static HttpPostedFile GetPostedFile(string name)
        {
            return Request.Files[name];
        }
        /// <summary> 
        /// 获取指定参数名的文件数组 
        /// </summary> 
        /// <param name="name"></param> 
        /// <returns></returns> 
        public static HttpPostedFile[] GetPostedFileArray(string name)
        {
            List<HttpPostedFile> list = new List<HttpPostedFile>();
            string[] names = Request.Files.AllKeys;
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i] == name)
                {
                    list.Add(Request.Files.Get(i));
                }
            }
            return list.ToArray();
        }

        /// <summary> 
        /// 获得当前页面客户端的IP 
        /// </summary> 
        /// <returns>当前页面客户端的IP</returns> 
        public static string GetIp()
        {
            string result = String.Empty;

            result = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = Request.ServerVariables["REMOTE_ADDR"];
            }

            if (string.IsNullOrEmpty(result))
            {
                result = Request.UserHostAddress;
            }

            if (string.IsNullOrEmpty(result))
            {
                return "127.0.0.1";
            }

            return result;
        }

        /// <summary> 
        /// 得到主机头 
        /// </summary> 
        /// <returns></returns> 
        public static string GetHost()
        {
            return Request.Url.Host;
        }

        /// <summary> 
        /// 获得当前完整Url地址 
        /// </summary> 
        /// <returns>当前完整Url地址</returns> 
        public static string GetUrl()
        {
            return Request.Url.ToString();
        }

        /// <summary> 
        /// 判断当前页面是否接收到了Post请求 
        /// </summary> 
        /// <returns>是否接收到了Post请求</returns> 
        public static bool IsPost()
        {
            return Request.HttpMethod.Equals("POST");
        }

        /// <summary> 
        /// 判断当前页面是否接收到了Get请求 
        /// </summary> 
        /// <returns>是否接收到了Get请求</returns> 
        public static bool IsGet()
        {
            return Request.HttpMethod.Equals("GET");
        }
    }
}