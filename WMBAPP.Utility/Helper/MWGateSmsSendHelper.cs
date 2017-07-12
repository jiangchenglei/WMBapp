using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

namespace Bill.Utility.Helper
{
    public class MWGateSmsSendHelper
    {
        private const string _MWGateAPIUrl = @"http://61.145.229.28:8027/MWGate/wmgw.asmx/{0}";

        /// <summary>
        /// 批量发送短信
        /// </summary>
        /// <param name="mobileList"></param>
        /// <param name="message"></param>
        public static string SMSBatchSend(List<string> mobileList, string message)
        {
            string result = string.Empty;
            if (mobileList != null && mobileList.Count > 0)
            {
                NameValueCollection data = new NameValueCollection();
                data.Add("userId", "M10185");
                data.Add("password", "589617");
                data.Add("pszMsg", message);
                data.Add("pszSubPort", "*");
                data.Add("MsgId", "1");
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                //byte[] serviceData = client.UploadValues(string.Format(_MWGateAPIUrl, "MongateSendSubmit"), data);
                List<string> ValidatemobileList = new List<string>();
                foreach (var item in mobileList)
                {
                    if (!string.IsNullOrWhiteSpace(item) && ValidateHelper.IsMobileNumber(item))
                    {
                        ValidatemobileList.Add(item);
                    }
                }

                if (ValidatemobileList.Count > 100)
                {
                    int index = ValidatemobileList.Count % 100;
                    for (int i = 0; i < index; i++)
                    {
                        List<string> newMobileList = new List<string>();
                        if (i == index - 1)
                        {
                            newMobileList = ValidatemobileList.Skip(i * 100).ToList();
                        }
                        else
                        {
                            newMobileList = ValidatemobileList.Skip(i * 100).Take(100).ToList();
                        }
                        data.Add("pszMobis", string.Join(",", newMobileList));
                        data.Add("iMobiCount", newMobileList.Count.ToString());
                        byte[] serviceData = client.UploadValues(string.Format(_MWGateAPIUrl, "MongateSendSubmit"), data);
                        result = Encoding.UTF8.GetString(serviceData);
                        FileTool.WriteLog(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "/Log/SmsSendLog/", string.Format("发送状态：{0}\n MobileList:{1}", result, JsonHelper.Serializer(newMobileList)));
                    }
                }
                else
                {
                    data.Add("pszMobis", string.Join(",", ValidatemobileList));
                    data.Add("iMobiCount", ValidatemobileList.Count.ToString());
                    byte[] serviceData = client.UploadValues(string.Format(_MWGateAPIUrl, "MongateSendSubmit"), data);
                    result = Encoding.UTF8.GetString(serviceData);
                    FileTool.WriteLog(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "/Log/SmsSendLog/", string.Format("发送状态：{0}\n m+MobileList:{1}", result, JsonHelper.Serializer(mobileList)));
                }

                return result;
                //string result = Encoding.UTF8.GetString(serviceData);
                //if (result == "-1")//参数为空。信息、电话号码等有空指针，登陆失败
                //{

                //}
                //else if (result == "-12")//有异常电话号码
                //{

                //}
                //else if (result == "-14")//实际号码个数超过100
                //{

                //}
                //else if (result == "-999")//web服务器内部错误
                //{

                //}
                //else//发送成功：平台消息编号，如：-8485643440204283743或1485643440204283743
                //{

                //}
            }
            return "-1";
        }

        /// <summary>
        /// 短信批量发送状态  返回xml
        /// </summary>
        public static string GetSMSBatchSend()
        {
            NameValueCollection data = new NameValueCollection();
            data.Add("userId", "M10185");
            data.Add("password", "589617");
            data.Add("iReqType", "2");
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] serviceData = client.UploadValues(string.Format(_MWGateAPIUrl, "MongateGetDeliver"), data);
            string result = Encoding.UTF8.GetString(serviceData);
            return result;
        }
    }
}
