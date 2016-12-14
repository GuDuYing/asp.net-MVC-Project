using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vCloud
{
    /// <summary>
    /// 获取上传加速节点地址输出参数的封装类
    /// </summary>
    public class GetUploadHostParam
    {
        /** 输出参数中的lbs*/
        public string lbs { get; set; }

        /** 输出参数中的上传节点地址集合*/
        public List<string> upload { get; set; }

        /** 输出参数中的响应码*/
        public int code { get; set; }

        /** 输出参数中的错误信息*/
        public string Message { get; set; }
    }
}
