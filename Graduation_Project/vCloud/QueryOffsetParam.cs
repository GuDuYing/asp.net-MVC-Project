using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vCloud
{
    /// <summary>
    /// 断点续传查询断点输出参数的封装类
    /// </summary>
    public class QueryOffsetParam
    {
        /** 输出参数中的uid字符串，服务器端生成的唯一UUID，用于记录日志排查问题使用*/
        public string requestId { get; set; } 

        /** 输出参数中的下一个上传片在上传块中的偏移*/
        public long offset { get; set; } 

        /** 输出参数中的上传上下文*/
        public string context { get; set; } 

        /** 输出参数中的错误信息*/
        public string errMsg { get; set; } 

        /** 输出参数中的上传回调信息*/
        public string callbackRetMsg { get; set; } 
    }
}
