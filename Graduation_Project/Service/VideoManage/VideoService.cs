using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IService.VideoManage;
using vCloud;

namespace Service.VideoManage
{
    public class VideoService:IVideoService
    {
        /* 输入个人信息 */
        /* 开发者平台分配的appkey 和 appSecret */
        private static string appKey = string.Empty;
        private static string appSecret = string.Empty;

        private static Credentials credentials = new Credentials(appKey,appSecret);
        
        private VcloudClient vclient = new VcloudClient(credentials);

        VideoServiceHelper helper = new VideoServiceHelper();
        /// <summary>
        /// 上传视频服务函数,返回视频id
        /// </summary>
        /// <param name="uploadIP"></param>
        /// <param name="videoName"></param>
        /// <param name="data"></param>
        /// <param name="filelength"></param>
        /// <returns></returns>
        public string UploadVideo(string uploadIP,string videoName,Stream data,long filelength)
        {
            IDictionary<string, object> initParamMap = helper.InitParam(videoName);
            /*视频上传初始化*/
            /*视频上传初始化返回结果的封装类*/
            InitUploadVideoParam initUploadVideoParam = vclient.initUploadVideo(initParamMap);

            if (initUploadVideoParam.code != 200)
            {
                throw new VcloudException("[VcloudClient]上传初始化失败 return code : " + initUploadVideoParam.code + " return message: " + initUploadVideoParam.msg);
            }
            /*获取上传加速节点地址*/
            /*获取上传加速节点地址返回结果的封装类*/
            GetUploadHostParam getUploadHostParam = vclient.getUploadHost(initUploadVideoParam);
            if (null == getUploadHostParam || null == getUploadHostParam.upload || getUploadHostParam.upload.Count == 0)
            {
                throw new VcloudException("[VcloudClient]获取上传加速节点地址失败 return code: " + getUploadHostParam.code + " return message: " + getUploadHostParam.Message);
            }
            try
            {
                /*分片上传视频*/
                /*当前分片在整个对象中的起始偏移量 */
                long offset = 0;
                /*上传上下文*/
                string context = null;
                /* 纪录上传文件的剩余大小*/
                long remainderSize = filelength;
                /*分片上传视频*/
                while (remainderSize > 0)
                {
                    
                    UploadVideoFragmentParam uploadVideoParam =  vclient.uploadVideoFragment(initUploadVideoParam, getUploadHostParam, offset, context, data, remainderSize);

                    if (null == uploadVideoParam.errMsg || "".Equals(uploadVideoParam.errMsg.Trim()))
                    {
                        context = uploadVideoParam.context;
                        offset = uploadVideoParam.offset;
                        remainderSize = filelength - offset;
                        // Console.WriteLine("uploadVideoParam.errMsg:" + uploadVideoParam.errMsg);
                    }
                    else
                    {
                        throw new VcloudException("upload videoFragment fail. errMsg: " + uploadVideoParam.errMsg + " requestID: " + uploadVideoParam.requestId);
                    }

                }
                if (remainderSize == 0)
                {
                    /* 查询上传视频的vid*/
                    List<string> objectNamesList = new List<string>();
                    objectNamesList.Add(initUploadVideoParam.ret.objectName);
                    /*查询上传视屏返回结果的封装类*/
                    QueryVideoIDorWatermarkIDParam queryVideoIDParam = vclient.queryVideoID(objectNamesList);
                    return queryVideoIDParam.ret.list[0].vid.ToString();
                }
                else
                {
                    throw new VcloudException("文件未传输完成");
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
