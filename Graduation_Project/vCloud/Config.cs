using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vCloud
{
    /// <summary>
    /// 项目中公共参数配置类
    /// </summary>
    public class Config
    {
        /** 分片上传视频：分片字节数（默认4M不变） */
        private static long uploadDataSize = 1024 * 1024 * 4;

        public static long UploadDataSize
        {
            get { return uploadDataSize; }
            set { uploadDataSize = value; }
        }

        /** 连接超时时限  */
        private static int connectionTimeout;

        public static int ConnectionTimeout
        {
            get { return connectionTimeout; }
            set { connectionTimeout = value; }
        }

        /* 开发者平台分配的appkey */
        private static string appKey;

        public static string AppKey
        {
            get { return appKey; }
            set { appKey = value; }
        }

        /* 开发者平台分配的appSecret */
        private static string appSecret;

        public static string AppSecret
        {
            get { return appSecret; }
            set { appSecret = value; }
        }
    }
}
