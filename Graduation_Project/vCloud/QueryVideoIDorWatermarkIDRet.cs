using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vCloud
{
    /// <summary>
    /// 根据对象名查询视频ID输出参数的ret部分的封装类
    /// </summary>
    public class QueryVideoIDorWatermarkIDRet
    {
        /** 输出参数中ret部分的vid个数*/
        public long count { get; set; } 

        /** 输出参数中ret部分的list*/
        public List<QueryVideoIDorWatermarkIDListParam> list { get; set; } 
    }
}
