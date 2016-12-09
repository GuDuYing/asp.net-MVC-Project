using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.VideoManage
{
    class VideoService
    {
        /// <summary>
        /// 上传视频服务函数
        /// </summary>
        /// <param name="uploadIP">客户端ip</param>
        /// <param name="videoName">上传视频名称</param>
        /// <param name="fs">上传视频文件流</param>
        /// <returns></returns>
        public string UploadVideo(string uploadIP,string videoName,FileStream fs)
        {
            string url = VideoHelper.videoUploadInit(videoName, uploadIP, 0);
            JsonOut json = VideoHelper.jsonGet(VideoHelper.DoGet(url));
            string code = json.code;
            string mes = json.message;
            string total = json.total;
            string token = json.data.token;
            if (!code.Equals("0"))
            {
                Console.Write("code = 0! Initial failed!");
                return "Error";
            }
            Console.Write("Initial Success!");
            Uri uploadUrl = new Uri(json.data.upload_url);
            byte[] uploadresult = VideoHelper.uploadPost(uploadUrl, fs);
            Console.Write(System.Text.Encoding.UTF8.GetString(uploadresult));
            Console.Write(uploadresult);
            Console.Write("Upload Success!");
            return json.data.video_id;
        }
    }
}
