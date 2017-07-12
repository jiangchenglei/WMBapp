using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bill.Utility.Helper
{
    public class JsonHelper
    {
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serializer(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("json", "序列化对象不能为空");
            return JsonConvert.SerializeObject(obj);
        }

        public static T Deserializer<T>(string json)
        {
            //if (string.IsNullOrEmpty(json))
            //    throw new ArgumentNullException("json", "反序列化对像不能为空");
            object obj = JsonConvert.DeserializeObject(json, typeof(T));
            T result = default(T);
            if (obj is T)
            {
                result = (T)obj;
            }
            return result;
        }
    }
}
