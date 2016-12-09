using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Commons.Log;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace Service.VideoManage
{
    public struct JsonOut
    {
        public string code { get; set; }
        public string message { get; set; }
        public string total { get; set; }
        public JsonIn data;
    }

    public struct JsonIn
    {
        public string video_id { get; set; }
        public string video_unique { get; set; }
        public string upload_url { get; set; }
        public string progress_url { get; set; }
        public string token { get; set; }
        public string uploadtype { get; set; }
        public string isdrm { get; set; }
    }

    public class VideoHelper
    {
        //用户唯一标识码
        private static string user_unique = "";   
        //用户密钥 
        private static string secretKey = "";
        //接口地址
        private static string URL = "http://api.letvcloud.com/open.php";
        //返回参数的格式
        private static string format = "JSON";
        //协议版本号
        private static string ver = "2.0";
        //视频上传标识，用于断点续传和上传进度查询
        private static string token;

        /// <summary>
        /// 上传视频初始化操作
        /// </summary>
        /// <param name="video_name">视频名称</param>
        /// <param name="client_ip">客户端ip</param>
        /// <param name="file_size">文件大小</param>
        /// <returns></returns>
        public static string videoUploadInit(String video_name, String client_ip, int file_size)
        {
            string api = "video.upload.init";
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("video_name", video_name);
            if (client_ip.Length > 0)
            {
                args.Add("client_ip", client_ip);
            }
            if (file_size > 0)
            {
                args.Add("file_size", file_size + "");
            }
            return MakeRequestURL(api, args);
        }


        public static string MakeRequestURL(string api,Dictionary<string,string> args)
        {
            args.Add("user_unique", user_unique);
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            string timestamp = Convert.ToInt64(ts.TotalSeconds).ToString();
            args.Add("timestamp", timestamp);
            args.Add("api", api);
            args.Add("format", format);
            args.Add("ver", ver);
            args.Add("sign",GenerateSign(args));

            //生成请求URL
            string requestUrl = "";
            requestUrl += URL + "?" + mapToQueryString(args);
            return requestUrl;

        } 

        /// <summary>
        /// 计算生成sign值
        /// </summary>
        /// <param name="args">参数信息字典</param>
        /// <returns>sign值</returns>
        private static string GenerateSign(Dictionary<string,string> args)
        {
            Dictionary<string, string> argsAsc = args.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            String keyStr = "";
            foreach (var key in argsAsc.Keys)
            {
                keyStr += key.ToString() + argsAsc[key];
            }
            keyStr += secretKey;
            return MD5Encrypt(keyStr);
        }

        /// <summary>
        /// 对字符串进行MD5加密
        /// </summary>
        /// <param name="strText">待加密字符串</param>
        /// <returns>已经加密的字符串</returns>
        private static string MD5Encrypt(string strText)
        {
            char[] md5Chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strText));
            char[] chars = new char[result.Length * 2];
            int i = 0;
            foreach (byte b in result)
            {
                char c0 = md5Chars[(b & 0xf0) >> 4];
                chars[i++] = c0;
                char c1 = md5Chars[b & 0xf];
                chars[i++] = c1;
            }
            return new String(chars);
        }

        /// <summary>
        /// 将参数信息字典转化为URL中的字符串
        /// </summary>
        /// <param name="args">参数信息字典</param>
        /// <returns>URL中的字符串</returns>
        private static string mapToQueryString(Dictionary<String, String> args)
        {
            List<string> keyList = new List<string>(args.Keys);
            keyList.Sort();
            string str = "";
            for (int i = 0; i < keyList.Count; i++)
            {
                string key = keyList[i];
                if (i != keyList.Count - 1)
                {
                    str += key + "=" + args[key] + "&";
                }
                else
                {
                    str += key + "=" + args[key];
                }//异常没有写
            }
            return str;
        }

        /// <summary>
        /// HTTP Get方法
        /// </summary>
        /// <param name="url">访问的URL地址</param>
        /// <returns></returns>
        public static string DoGet(String url)
        {
            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "GET";
                using (var response = request.GetResponse())
                using (var responseStream = response.GetResponseStream())
                using (var mstream = new MemoryStream())
                {
                    responseStream.CopyTo(mstream);
                    return Encoding.UTF8.GetString(mstream.ToArray());
                }
            }
            catch (Exception e)
            {
                Console.Write("GET 请求失败！");
                return "";
            }
        }

        /// <summary>
        /// 将HTTP Get方法请求返回的数据转化为JSON格式的数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static JsonOut jsonGet(string str)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            JsonOut json = serializer.Deserialize<JsonOut>(str);
            return json;
        }

        /// <summary>
        /// 用于上传视频的POST方法
        /// </summary>
        /// <param name="uri">请求URL</param>
        /// <param name="file">上传视频的文件流</param>
        /// <returns></returns>
        public static byte[] uploadPost(Uri uri, FileStream file)
        {
            string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            request.Credentials = CredentialCache.DefaultCredentials;
            MemoryStream stream = new MemoryStream();
            byte[] line = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endline = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            //提交文件
            if (file != null)
            {
                string fformat = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" + Environment.NewLine + "Content-Type: application/octet-stream"
                    + Environment.NewLine + Environment.NewLine;
                string s = string.Format(fformat, file.Name, file.Name);
                byte[] sdata = Encoding.ASCII.GetBytes(s);
                stream.Write(sdata, 0, sdata.Length);
                byte[] filedata = new byte[file.Length];
                file.Read(filedata, 0, filedata.Length);
                stream.Write(filedata, 0, filedata.Length);
                stream.Write(endline, 0, endline.Length);
            }
            request.ContentLength = stream.Length;
            Stream requestStream = request.GetRequestStream();
            stream.Position = 0L;
            stream.CopyTo(requestStream);
            stream.Close();
            requestStream.Close();
            using (var response = request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var mstream = new MemoryStream())
            {
                responseStream.CopyTo(mstream);
                return mstream.ToArray();
            }
        }
    }
}
