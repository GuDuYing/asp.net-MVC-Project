using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IService.VideoManage;

namespace Service.VideoManage
{
    class VideoService:IVideoService
    {
        /// <summary>
        /// 上传视频服务函数,返回视频id
        /// </summary>
        /// <param name="uploadIP"></param>
        /// <param name="videoName"></param>
        /// <param name="data"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public string UploadVideo(string uploadIP,string videoName,byte[] data,int max)
        {
            return null;
        }
    }
}
