using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vCloud
{
    /// <summary>
    ///  视频上传初始化输出参数中ret部分的封装类
    /// </summary>
    public class InitUploadVideoRet
    {
        /** 输出参数中的上传的token信息*/
        public String xNosToken { get; set; }

        /** 输出参数中的存储上传文件的桶名*/
        public String bucket { get; set; }

        /** 输出参数中的存储上传文件的对象名*/
        public String objectName { get; set; }

    }
}
