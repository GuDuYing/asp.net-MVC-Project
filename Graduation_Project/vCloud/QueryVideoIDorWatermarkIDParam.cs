using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vCloud
{
    /// <summary>
    /// 根据对象名查询视频ID输出参数的封装类
    /// </summary>
    public class QueryVideoIDorWatermarkIDParam
    {
        /** 输出参数中的响应码*/
        public int code { get; set; } 

        /** 输出参数中的ret部分*/
        public QueryVideoIDorWatermarkIDRet ret { get; set; } 

        /** 输出参数中的错误信息*/
        public string msg { get; set; } 
    }
}
