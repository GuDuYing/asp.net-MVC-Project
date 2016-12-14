using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace vCloud
{
    public class HttpClientBuilder
    {
        /// <summary>
        /// Post请求接口
        /// </summary>
        /// <param name="url">接口url</param>
        /// <param name="json">json格式的数据内容</param>
        /// <returns>返回响应的json数据</returns>
        public static string HttpPost(string url, string json)
        {
            System.Net.WebRequest request = httpRequestBuilder(url, "POST", null);           

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);                
                streamWriter.Flush();
                streamWriter.Close();
            }

            HttpWebResponse response = null;

            try
            {
                // Get the response.
                response = (HttpWebResponse)request.GetResponse();
                // Display the status.
                //Console.WriteLine(response.StatusDescription);
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine(responseFromServer);
                // Cleanup the streams and the response.
                reader.Close();
                dataStream.Close();
                response.Close();
                return responseFromServer;
            }
            catch (Exception e)
            {
                Console.WriteLine("[HttpPost] fail to get response. " + e.Message);
                Console.WriteLine("[HttpPost] fail to get response. " + response.StatusDescription);
                throw new VcloudException("[HttpPost] fail to get response. " + e.Message);               
            }
        }

        /// <summary>
        /// Get请求接口
        /// </summary>
        /// <param name="url">接口url</param>
        /// <returns>返回响应的json数据</returns>
        public static string HttpGet(string url)
        {
            System.Net.WebRequest request = httpRequestBuilder(url, "GET", null);                  

            HttpWebResponse response = null;
            try
            {
                // Get the response.
                response = (HttpWebResponse)request.GetResponse();
                // Display the status.
               // Console.WriteLine(response.StatusDescription);
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
               // Console.WriteLine(responseFromServer);
                // Cleanup the streams and the response.
                reader.Close();
                dataStream.Close();
                response.Close();
                return responseFromServer;
            }
            catch (Exception e)
            {
                Console.WriteLine("[HttpPost] fail to get response. " + e.Message);
                Console.WriteLine("[HttpPost] fail to get response. " + response.StatusDescription);
                throw new VcloudException("[HttpPost] fail to get response. " + e.Message);  
            }
        }
        /// <summary>
        /// Get请求接口
        /// </summary>
        /// <param name="url">接口url</param>
        /// <param name="xNosToken">上传token</param>
        /// <returns></returns>
        public static string HttpGetWithXNosToken(string url, string xNosToken)
        {
            System.Net.WebRequest request = httpRequestBuilder(url, "GET", xNosToken);

            HttpWebResponse response = null;
            try
            {
                // Get the response.
                response = (HttpWebResponse)request.GetResponse();
                // Display the status.
                // Console.WriteLine(response.StatusDescription);
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine(responseFromServer);
                // Cleanup the streams and the response.
                reader.Close();
                dataStream.Close();
                response.Close();
                return responseFromServer;
            }
            catch (Exception e)
            {
                Console.WriteLine("[HttpPost] fail to get response. " + e.Message);
                Console.WriteLine("[HttpPost] fail to get response. " + response.StatusDescription);
                throw new VcloudException("[HttpPost] fail to get response. " + e.Message);
            }
        }

        /// <summary>
        /// 上传视频分片的Post请求
        /// </summary>
        /// <param name="url">接口url</param>
        /// <param name="buffer">上传的字符数组</param>
        /// <param name="xNosToken">上传token</param>
        /// <returns>返回响应的json数据</returns>
        public static string HttpPostVideo(string url, byte[] buffer, string xNosToken)
        {
            //得到request
            System.Net.WebRequest request = httpRequestBuilder(url, "POST", xNosToken);

            using (Stream streamWriter = request.GetRequestStream())
            {
                streamWriter.Write(buffer, 0, buffer.Length);
                streamWriter.Flush();
                streamWriter.Close();
            }

            HttpWebResponse response = null;

            try
            {
                // Get the response.
                response = (HttpWebResponse)request.GetResponse();
                // Display the status.
                //Console.WriteLine(response.StatusDescription);
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                // Console.WriteLine(responseFromServer);
                // Cleanup the streams and the response.
                reader.Close();
                dataStream.Close();
                response.Close();
                return responseFromServer;
            }
            catch (Exception e)
            {
                Console.WriteLine("[HttpPost] fail to get response. " + e.Message);
                Console.WriteLine("[HttpPost] fail to get response. " + response.StatusDescription);
                throw new VcloudException("[HttpPost] fail to get response. " + e.Message);  
            }
        }

        /// <summary>
        /// 构建request
        /// </summary>
        /// <param name="url">接口url</param>
        /// <param name="Method">请求方式</param>
        /// <param name="xNosToken">上传token</param>
        /// <returns></returns>
        public static WebRequest httpRequestBuilder(string url, string Method, string xNosToken)
        {
            System.Net.WebRequest request = HttpWebRequest.Create(url);
            string appKey = Config.AppKey;
            string appSecret = Config.AppSecret;
            if (appKey == null || appSecret == null)
            {
                throw new VcloudException("[HttpClientBuilder] fail to read appKey or appSecret");
            }

            //Console.WriteLine("url:{0}",url);
            String nonce = "1";//TO开发者：具体参考开发文档

            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);
            Int32 ticks = System.Convert.ToInt32(ts.TotalSeconds);
            String curTime = ticks.ToString();
            String checkSum = CheckSumBuilder.getCheckSum(appSecret, nonce, curTime);

            IDictionary<object, String> headers = new Dictionary<object, String>();
            headers["AppKey"] = appKey;
            headers["Nonce"] = nonce;
            headers["CurTime"] = curTime;
            headers["CheckSum"] = checkSum;
            headers["ContentType"] = "application/json;charset=utf-8";
            if (null != xNosToken)
            {
                headers["x-nos-token"] = xNosToken;
            }   
            request.ContentType = "application/json";
            request.Method = Method;

            request.Timeout = Config.ConnectionTimeout;

            if (headers != null)
            {
                foreach (var v in headers)
                {
                    if (v.Key is HttpRequestHeader)
                        request.Headers[(HttpRequestHeader)v.Key] = v.Value;
                    else
                        request.Headers[v.Key.ToString()] = v.Value;

                }
            }
            return request;
        }
    }
}
