using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vCloud
{
    /// <summary>
    /// Vcloud自定义的异常类
    /// </summary>
    [Serializable]
    public class VcloudException : ApplicationException
    {
        /// <summary>  
        /// 默认构造函数  
        /// </summary>  
        public VcloudException() { }

        public VcloudException(string message)
            : base(message) { }

        public VcloudException(string message, Exception inner)
            : base(message, inner) { }
    }
}
