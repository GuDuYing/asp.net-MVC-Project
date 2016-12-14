using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vCloud
{
    /// <summary>
    /// 根据对象名查询视频ID输出参数的ret部分的list的封装类
    /// </summary>
    public class QueryVideoIDorWatermarkIDListParam
    {
        /** 输出参数中的存储上传文件的对象名*/
        public string objectName { get; set; } 

        /** 输出参数中的视频ID*/
        public long vid { get; set; } 

        /** 输出参数中的图片ID*/
        public long imgId { get; set; } 
    }
}
