using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Bill.Utility.Helper
{
    public class HttpClientHelper
    {
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        /// <summary>  
        /// 创建GET方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="timeout">请求的超时时间</param>  
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>   
        /// <returns></returns>  
        public static string HttpGetRequest(string url, string userAgent, int timeout = 15000)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url is empty");
            }
            if (timeout <= 0)
                throw new ArgumentException("timeout <= 0.");
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            request.Timeout = timeout;

            HttpWebResponse resp;
            StreamReader sr;
            string result;

            resp = (HttpWebResponse)request.GetResponse();
            sr = new StreamReader(resp.GetResponseStream());

            result = sr.ReadToEnd();

            return result;
        }  

        public static string HttpPostRequest(string address, string postData, int timeout = 15000)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("address is null or empty.");

            if (string.IsNullOrEmpty(postData))
                throw new ArgumentException("postData is null or empty.");

            if (timeout <= 0)
                throw new ArgumentException("timeout <= 0.");

            byte[] data;
            data = Encoding.UTF8.GetBytes(postData);

            System.Net.HttpWebRequest req;
            System.IO.Stream reqStream;

            req = (HttpWebRequest)HttpWebRequest.Create(address);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.Timeout = timeout;
            req.ContentLength = data.Length;

            reqStream = req.GetRequestStream();
            reqStream.Write(data, 0, data.Length);//发送 

            HttpWebResponse resp;
            StreamReader sr;
            string result;

            resp = (HttpWebResponse)req.GetResponse();
            sr = new StreamReader(resp.GetResponseStream());

            result = sr.ReadToEnd();

            return result;
        }


        /// <summary>
        /// 带有referer请求头信息的HTTP方式请求数据
        /// 张思道 2015-03-16
        /// </summary>
        /// <param name="url">目标URL地址(必须以http://或https://开头)</param>
        /// <param name="query">URL地址参数</param>
        /// <param name="method">请求方式(get和method)</param>
        /// <param name="referer">上一个URL</param>
        /// <param name="host">主机头</param>
        public static string HttpRequestData(string url, string query, string method, string referer, string host, int timeout = 15000)
        {
            if (timeout <= 0)
                throw new ArgumentException("timeout <= 0.");

            HttpWebRequest httpReq;

            string result = "";

            //Get和Post方式
            if (method == "get")
            {
                string requesturl = url;

                if (query != "")
                {
                    requesturl += "?" + query;
                }

                httpReq = (HttpWebRequest)HttpWebRequest.Create(requesturl);
                httpReq.Method = "get";

                //部分请求头信息
                httpReq.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:13.0) Gecko/20100101 Firefox/13.0.1";
                if (!string.IsNullOrEmpty(referer))
                {
                    httpReq.Referer = referer;
                }
                if (!string.IsNullOrEmpty(host))
                {
                    httpReq.Host = host;
                }
                httpReq.KeepAlive = true;

                httpReq.Timeout = timeout;


                HttpWebResponse httpRes = (HttpWebResponse)httpReq.GetResponse();
                Stream myRequestStream = httpRes.GetResponseStream();
                StreamReader myStreamRead = new StreamReader(myRequestStream);
                result = myStreamRead.ReadToEnd();

            }
            else if (method == "post")
            {
                httpReq = (HttpWebRequest)HttpWebRequest.Create(url);
                httpReq.Method = "Post";
                httpReq.ContentType = "application/x-www-form-urlencoded;charset=gb2312";

                if (query != "")
                {
                    byte[] postBytes = Encoding.ASCII.GetBytes(query);
                    httpReq.ContentLength = postBytes.Length;
                }

                //部分请求头信息
                httpReq.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:13.0) Gecko/20100101 Firefox/13.0.1";
                if (!string.IsNullOrEmpty(referer))
                {
                    httpReq.Referer = referer;
                }
                if (!string.IsNullOrEmpty(host))
                {
                    httpReq.Host = host;
                }
                httpReq.KeepAlive = true;

                httpReq.Timeout = timeout;


                //请求并获取数据
                using (StreamWriter myWriter = new StreamWriter(httpReq.GetRequestStream()))
                {
                    myWriter.Write(query);
                }

                using (WebResponse response = httpReq.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        result = sr.ReadToEnd();
                    }
                }

            }

            return result;
        }



        public static WebClient GetWebClient()
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers.Add("Accept", "*/*");
            return client;
        }

        protected static Encoding GetEncoding(string ContentType, WebClient client)
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

        public static string TakeString(string url, NameValueCollection data)
        {
            try
            {
                WebClient client = GetWebClient();
                byte[] serviceData = client.UploadValues(url, data);
                Encoding contentEncoding = GetEncoding(client.ResponseHeaders["Content-Type"], client);
                string result = contentEncoding.GetString(serviceData);
                return result;
            }
            catch (WebException ex)
            {
                HttpWebResponse res = (HttpWebResponse)ex.Response;
                if (res != null)
                {
                    StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                    return sr.ReadToEnd();
                }
                else
                {
                  
                    return "";
                
                }
            }
        }
    }
}
