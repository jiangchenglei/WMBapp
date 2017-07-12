using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bill.Utility.Helper
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class EnumAttribute : Attribute
    {
        private string _name;
        private string _description;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public EnumAttribute(string name)
        {
            this.Name = name;
        }

        public EnumAttribute(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }



    public class EnumHelper
    {
        /// <summary>
        /// 用于缓存枚举值的属性值
        /// </summary>
        private static readonly Dictionary<object, EnumAttribute> enumAttr = new Dictionary<object, EnumAttribute>();


        /// <summary>
        /// 获取枚举值的名称，该名称由EnumAttribute定义
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <returns>枚举值对应的名称</returns>
        public static string GetName(Enum value)
        {
            EnumAttribute ea = GetAttribute(value);
            return ea != null ? ea.Name : "";
        }
        /// <summary>
        /// 获取枚举值内容
        /// 2016-08-02
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static string GetStringName<T>(T value)
        {
            string result = "";
            try
            {
                result = Enum.GetName(typeof(T), value);
            }
            catch { }
            return result;
        }

        /// <summary>
        /// 获取枚举值的名称，该名称由EnumAttribute定义
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <returns>枚举值对应的名称</returns>
        public static string GetDescription(Enum value)
        {
            EnumAttribute ea = GetAttribute(value);
            return ea != null ? ea.Description : "";
        }


        /// <summary>
        /// 获取枚举类型的 值-名称 列表
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetValueName(Type enumType)
        {
            Type underlyingType = Enum.GetUnderlyingType(enumType);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (object o in Enum.GetValues(enumType))
            {
                Enum e = (Enum)o;
                string value = Convert.ChangeType(o, underlyingType).ToString();
                dic.Add(value, GetName(e));
            }
            return dic;
        }

        /// <summary>
        /// 从字符串转换为枚举类型
        /// </summary>
        /// <typeparam name="T">枚举类型（包括可空枚举）</typeparam>
        /// <param name="str">要转为枚举的字符串</param>
        /// <exception cref="Exception">转换失败</exception>
        /// <returns>转换结果</returns>
        public static T GetEnum<T>(string str)
        {
            Type type = typeof(T);

            Type nullableType = Nullable.GetUnderlyingType(type);
            if (nullableType != null) type = nullableType;

            Type underlyingType = Enum.GetUnderlyingType(type);
            object o = Convert.ChangeType(str, underlyingType);

            if (!Enum.IsDefined(type, o)) throw new Exception("枚举类型\"" + type.ToString() + "\"中没有定义\"" + (o == null ? "null" : o.ToString()) + "\"");

            //处理可空枚举类型
            if (nullableType != null)
            {
                ConstructorInfo c = typeof(T).GetConstructor(new Type[] { nullableType });
                return (T)c.Invoke(new object[] { o });
            }
            return (T)o;
        }

        /// <summary>
        /// 从字符串转换为枚举类型
        /// </summary>
        /// <typeparam name="T">枚举类型（包括可空枚举）</typeparam>
        /// <param name="str">要转为枚举的字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns>转换结果</returns>
        public static T GetEnum<T>(string str, T defaultValue)
        {
            try
            {
                return GetEnum<T>(str);
            }
            catch
            {
                return defaultValue;
            }
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
        /// 判断是否包含指定的值
        /// </summary>
        /// <param name="multValue"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsMarked(Enum multValue, Enum value)
        {
            return (Convert.ToInt32(multValue) & Convert.ToInt32(value)) == Convert.ToInt32(value);
        }

        /// <summary>
        /// 组合所有的值为一个
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static T Unite<T>(params T[] values)
        {
            int v = 0;
            foreach (T value in values)
            {
                v |= Convert.ToInt32(value);
            }
            return ConvertHelper.ToType<T>(v);
        }

        /// <summary>
        /// 将指定的值拆分为一个枚举值的数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static T[] GetValues<T>(Enum values)
        {
            List<T> l = new List<T>();
            foreach (Enum v in Enum.GetValues(typeof(T)))
            {
                if (IsMarked(values, v))
                {
                    l.Add((T)((object)v));
                }
            }
            return l.ToArray();
        }

        /// <summary>
        /// 获取枚举值定义的属性
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static EnumAttribute GetAttribute(Enum value)
        {
            if (enumAttr.ContainsKey(value))
            {
                EnumAttribute ea = enumAttr[value];
                return ea;
            }
            else
            {
                FieldInfo field = value.GetType().GetField(value.ToString());
                if (field == null) return null;
                EnumAttribute ea = null;
                object[] attributes = field.GetCustomAttributes(typeof(EnumAttribute), true);
                if (attributes != null && attributes.Length > 0)
                {
                    ea = (EnumAttribute)attributes[0];
                }
                enumAttr[value] = ea;
                return ea;
            }
        }
    }
}
