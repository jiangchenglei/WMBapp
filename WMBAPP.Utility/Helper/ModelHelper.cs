using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using WMB.Utility.Helper;

namespace Bill.Utility.Helper
{
    /// <summary>
    /// 模型辅助处理类
    /// 2016-04-18
    /// </summary>
    public class ModelHelper
    {
        /// <summary>
        /// 将数据转化为模型
        /// 2016-04-18 基础数据类型：Int32，decimal
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public static void ConvertToModel<T>(ref T t)
        {
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            Type[] tempArr = new Type[] { typeof(Int32), typeof(decimal) };
            object tempObject = null;
            for (Int32 i = 0; i < properties.Length; i++)
            {
                if (ReqUtils.FormString(properties[i].Name) != null || properties[i].PropertyType.Name.ToLower() == "httpfilecollection")
                {
                    switch (properties[i].PropertyType.Name.ToLower())
                    {

                        case "int32": tempObject = ConvertHelper.ToInt32(string.Format("{0}", ReqUtils.FormString(properties[i].Name))); break;
                        case "decimal": tempObject = ConvertHelper.ToDecimal(string.Format("{0}", ReqUtils.FormString(properties[i].Name))); break;
                        case "httpfilecollection": tempObject = System.Web.HttpContext.Current.Request.Files; break;
                        case "nullable`1":
                            if (properties[i].PropertyType.FullName.ToLower().Contains("int32"))
                            {
                                tempObject = ConvertHelper.ToInt32(string.Format("{0}", ReqUtils.FormString(properties[i].Name)));
                            }
                            else if (properties[i].PropertyType.FullName.ToLower().Contains("datetime"))
                            {
                                tempObject = ConvertHelper.ToDateTime(string.Format("{0}", ReqUtils.FormString(properties[i].Name)));
                            }
                            else
                            {
                                tempObject = null;
                            }
                            break;
                        case "boolean": tempObject = ConvertHelper.ToType<bool>(string.Format("{0}", ReqUtils.FormString(properties[i].Name))); break;//2016-08-19zc
                        default:
                            tempObject = string.Format("{0}", ReqUtils.FormString(properties[i].Name));
                            break;
                    }
                    properties[i].SetValue(t, tempObject, null);
                }
            }
        }
        /// <summary>
        /// 该实体是否都为空
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModelIsNotNull(object model)
        {
            bool istrue = true;
            Type type = model.GetType();
            var flags = GetOnlyProperty();
            var list = type.GetProperties(flags).ToList();
            foreach (var p in list)
            {
                if (IsColumn(p))
                {
                    object value = p.GetValue(model, null) ?? "";
                    if (string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        istrue = false;
                        break;
                    }
                }
            }
            return istrue;
        }
        private BindingFlags GetOnlyProperty()
        {
            return BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance;
        }
        private bool IsColumn(PropertyInfo info)
        {
            object[] objs = info.GetCustomAttributes(typeof(Attribute), true);
            return objs.Length == 0;
        }
        /// <summary>
        /// 获取操作提示翻译
        /// </summary>
        /// <param name="key"></param>
        /// <param name="lan"></param>
        /// <returns></returns>
        public static string GetTans(string key, string lan = "")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lan))
                {
                    lan = "cn";
                }
                if (lan.ToLower().Contains("cn") || lan.ToLower().Contains("zh"))
                {
                    lan = "zh";
                }
                string className = "PageInfo_" + lan;
                var globalResourceObject = HttpContext.GetGlobalResourceObject(className, key);
                if (globalResourceObject != null)
                    return globalResourceObject.ToString() == "" ? key : globalResourceObject.ToString();
                else
                {
                    return key;
                }
            }
            catch
            {
                return key;
            }
        }
        /// <summary>
        /// 订单推送相关公用方法 jcl add  2016 08-02
        /// </summary>
        /// <param name="UserID">推送人用户ID</param>
        /// <param name="ToUserID">推送给谁</param>
        /// <param name="Type"></param>
        /// <param name="OrderID"></param>
        /// <param name="lan"></param>
        //{0,"od_新订单"},<br />
        //{1,"od_提醒询盘"},<br />
        //{2,"od_询盘完成"},<br />
        //{3,"od_订单已确认"},<br />
        //{4,"od_提醒付款"},<br />
        //{5,"od_提醒发货"},<br />
        //{6,"od_提醒收货"},<br />
        //{18,"od_取消订单"},<br />
        //{41,"od_买家已经付款点击查看"}<br />
        public static void PushOrderInfo(int UserID, int ToUserID, string Type, string OrderID, string lan)
        {
            NameValueCollection data = new NameValueCollection();
            data.Add("lan", lan);
            data.Add("ToUserID", ToUserID.ToString());
            data.Add("OwnUserID", UserID.ToString());
            data.Add("Type", Type);
            data.Add("OrderID", OrderID.ToString());
            //ConfigurationManager.AppSettings["SiteDomain"].ToString() ?? 
            string Site = "http://oc.osell.com";
            var str = HttpClientHelper.TakeString(Site + "/Common/SendPushMessage", data);
            FileTool.WriteLog(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "/Log/PushOrderInfo/", "str=" + str);
        }


        /// <summary>
        /// 手机推送相关公用方法
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="SendType">消息类型</param>
        /// <param name="Content">消息内容</param>
        /// <param name="lan">语言</param>
        public static string SendApNsPushMessage(int UserID, int SendType, string Content, string lan)
        {
            NameValueCollection data = new NameValueCollection();
            data.Add("UserID", UserID.ToString());
            data.Add("SendType", SendType.ToString());
            data.Add("Content", Content);
            data.Add("lan", lan);
            string Site = "http://oc.osell.com";
            string result = HttpClientHelper.TakeString(Site + "/Common/SendApNsPushMessage", data);
            return result;
        }
    }
}
