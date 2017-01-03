using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService.VideoManage
{
    public interface IVideoService
    {

        /// <summary>
        /// 上传视频服务函数,返回视频id
        /// </summary>
        /// <param name="uploadIP"></param>
        /// <param name="videoName"></param>
        /// <param name="data"></param>
        /// <param name="filelength"></param>
        /// <returns></returns>
        string UploadVideo(string uploadIP, string videoName, Stream data,long filelength);
    }
}
