using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Bill.Utility.Helper
{
    public class HttpHelper
    {
        public WebClient GetWebClient()
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers.Add("Accept", "*/*");
            return client;
        }

        protected Encoding GetEncoding(string ContentType, WebClient client)
        {
            Encoding result = Encoding.UTF8;
            if (!string.IsNullOrEmpty(ContentType))
            {
                string[] contentTypes = ContentType.Split(':');
                foreach (string temp in contentTypes)
                {
                    string[] charset = temp.Trim().Split('=');
                    if (charset.Length == 2 && charset[0].Trim() == "charset")
                    {
                        result = Encoding.GetEncoding(charset[1].Trim());
                        client.Encoding = result;
                        break;
                    }
                }
            }
            return result;
        }


        public byte[] Post(string url, NameValueCollection data)
        {


            WebClient client = GetWebClient();
            byte[] serviceData = client.UploadValues(url, data);
            Encoding contentEncoding = GetEncoding(client.ResponseHeaders["Content-Type"], client);

            return serviceData;
        }

        public string TakeUploadFileString(string url, string FilePath)
        {
            WebClient client = GetWebClient();
            string fileName = FilePath;
            byte[] serviceData = client.UploadFile(url, fileName);
            Encoding contentEncoding = GetEncoding(client.ResponseHeaders["Content-Type"], client);
            string result = contentEncoding.GetString(serviceData);
            return result;
        }

        public string Get(string strURL)
        {
            System.Net.HttpWebRequest request;
            // 创建一个HTTP请求
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            //request.Method="get";
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string responseText = myreader.ReadToEnd();
            myreader.Close();
            return responseText;
        }

        public string TakeString(string url, NameValueCollection data)
        {

            WebClient client = GetWebClient();
            byte[] serviceData = client.UploadValues(url, data);
            Encoding contentEncoding = GetEncoding(client.ResponseHeaders["Content-Type"], client);
            string result = contentEncoding.GetString(serviceData);
            return result;
        }

        public string DownloadString(string url)
        {

            WebClient client = GetWebClient();
            return client.DownloadString(url);
        }

        /// <summary>
        /// post提交
        /// </summary>
        /// <param name="url">http url地址</param>
        /// <param name="paramsdata">参数 user=123&pwd=123</param>
        /// <returns></returns>
        public string PostWebRequest(string url, string paramsdata)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = Encoding.Default.GetBytes(paramsdata); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(url));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {

            }
            return ret;
        }
    }
}
