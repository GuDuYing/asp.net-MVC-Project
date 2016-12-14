using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vCloud
{
    /// <summary>
    /// 设置上传回调地址接口输出参数的封装类
    /// </summary>
    public class SetCallbackParam
    {
        /** 输出参数中的响应码*/
        public int code { get; set; }

        /** 输出参数中的错误信息*/
        public string msg { get; set; }
    }
}
