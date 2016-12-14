using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vCloud
{
    /// <summary>
    /// 视频上传初始化输入参数的封装类
    /// </summary>
    public class InitUploadVideoModule
    {
        /// <summary>
        ///上传文件的原始名称（包含后缀名） 此参数必填
        /// </summary>
        public string originFileName { get; set; }

        /// <summary>
        /// 用户命名的上传文件名称  此参数非必填
        /// </summary>
        public string userFileName { get; set; }

        /// <summary>
        ///  视频所属的类别ID（不填写为默认分类）此参数非必填
        /// </summary>
        public string typeId { get; set; }

        /// <summary>
        /// 视频所需转码模板ID（不填写为默认模板） 此参数非必填
        /// </summary> 
        public string presetId { get; set; }

        /// <summary>
        /// 转码成功后回调客户端的URL地址（需标准http格式）  此参数非必填
        /// </summary>
        public string callbackUrl { get; set; }

        /// <summary>
        ///  上传视频的描述信息  此参数非必填 
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 上传视频的视频水印Id 此参数非必填
        /// </summary>
        public string watermarkId { get; set; }

        /// <summary>
        ///  上传成功后回调客户端的URL地址（需标准http格式） 此参数非必填 
        /// </summary>
        public string uploadCallbackUrl { get; set; }

        /// <summary>
        /// 用户自定义信息，会在上传成功或转码成功后通过回调返回给用户 此参数非必填
        /// </summary>
        public string userDefInfo { get; set; }

        public InitUploadVideoModule()
        { }
        /// <summary>
        /// 传入初始化参数集合构造封装类
        /// </summary>
        /// <param name="initParamMap">初始化参数集合</param>
        public InitUploadVideoModule(IDictionary<String, Object> initParamMap)
        {
            if (initParamMap.ContainsKey("originFileName"))
            {
                this.originFileName = (string)initParamMap["originFileName"];
            }
            if (initParamMap.ContainsKey("userFileName"))
            {
                this.userFileName = (string)initParamMap["userFileName"];
            }
            if (initParamMap.ContainsKey("typeId"))
            {
                this.typeId = Convert.ToString(initParamMap["typeId"]);
            }
            if (initParamMap.ContainsKey("presetId"))
            {
                this.presetId = Convert.ToString(initParamMap["presetId"]);
            }
            if (initParamMap.ContainsKey("callbackUrl"))
            {
                this.callbackUrl = (string)initParamMap["callbackUrl"];
            }
            if (initParamMap.ContainsKey("description"))
            {
                this.description = (string)initParamMap["description"];
            }
            if (initParamMap.ContainsKey("watermarkId"))
            {
                this.watermarkId = Convert.ToString(initParamMap["watermarkId"]);
            }
            if (initParamMap.ContainsKey("uploadCallbackUrl"))
            {
                this.uploadCallbackUrl = (string)initParamMap["uploadCallbackUrl"];
            }
            if (initParamMap.ContainsKey("userDefInfo"))
            {
                this.userDefInfo = (string)initParamMap["userDefInfo"];
            }
        }
    }
}
