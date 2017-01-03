using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.VideoManage
{
    public class VideoServiceHelper
    {
        /// <summary>
        /// 初始化参数列表
        /// </summary>
        /// <param name="originfilename">上传的文件的原文件名</param>
        /// <returns></returns>
        public IDictionary<string,object> InitParam(string originfilename)
        {
            IDictionary<String, Object> initParamMap = new Dictionary<String, Object>();

            /*输入上传文件的相关信息 */
            /* 上传文件的原始名称（包含后缀名） 此参数必填*/
            initParamMap.Add("originFileName", originfilename);

            /* 用户命名的上传文件名称  此参数非必填*/
            initParamMap.Add("userFileName", originfilename);

            /* 视频所属的类别ID（不填写为默认分类）此参数非必填*/
            //initParamMap.Add("typeId", 1056);

            /* 频所需转码模板ID（不填写为默认模板） 此参数非必填*/
            //initParamMap.Add("presetId", 30599);

            /* 转码成功后回调客户端的URL地址（需标准http格式）  此参数非必填*/
            initParamMap.Add("callbackUrl", null);

            /* 上传视频的描述信息  此参数非必填*/
            //initParamMap.Add("description", "love.mp4");

            /* 上传视频的视频水印Id 此参数非必填*/
            //initParamMap.Add("watermarkId",1);	  

            /** 上传成功后回调客户端的URL地址（需标准http格式） */

            //initParamMap.Add("uploadCallbackUrl", "");

            /** 用户自定义信息，会在上传成功或转码成功后通过回调返回给用户 */
            //initParamMap.Add("userDefInfo", null); 
            return initParamMap; 
        }
    }
}
