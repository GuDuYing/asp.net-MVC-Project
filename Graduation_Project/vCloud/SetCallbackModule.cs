using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vCloud
{
    /// <summary>
    /// 设置上传回调地址接口输入参数的封装类
    /// </summary>
    public class SetCallbackModule
    {
        /**  上传成功后回调客户端的URL地址     参数必填*/
        public string callbackUrl { get; set; }

        public SetCallbackModule()
        { }

        /// <summary>
        /// 传入上传成功后回调客户端的URL地址构造封装类
        /// </summary>
        /// <param name="callbackUrl">上传成功后回调客户端的URL地址</param>
        public SetCallbackModule(string callbackUrl)
        {
            this.callbackUrl = callbackUrl;
        }
    }
}
