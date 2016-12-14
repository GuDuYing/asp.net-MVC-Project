using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vCloud
{
    /// <summary>
    /// 根据对象名查询视频或水印图片ID输入参数的封装类
    /// </summary>
    public class QueryVideoIDorWatermarkIDModule
    {
        /// <summary>
        ///  查询文件的对象名     参数必填
        /// </summary>
        public List<string> objectNames { get; set; }

        public QueryVideoIDorWatermarkIDModule()
        { }

        /// <summary>
        /// 传入查询文件的对象名构造封装类
        /// </summary>
        /// <param name="objectNamesList"> 查询文件的对象名</param>

        public QueryVideoIDorWatermarkIDModule(List<string> objectNamesList)
        {
            this.objectNames = objectNamesList;
        }
    }
}
