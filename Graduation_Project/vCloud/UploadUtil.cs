using Commons.JsonHelper;
using System;
using System.IO;

namespace vCloud
{
    /// <summary>
    /// 上传视频的工具类
    /// </summary>
    public class UploadUtil
    {
        /** 切片上传：最小片字节数（默认不变） */
        private long uploadDataSize = Config.UploadDataSize;

        /** 存储上传文件的桶名  */
        private string bucket;

        /** 上传加速节点地址 */
        private string uploadHost;

        /** 存储上传文件的对象名 */
        private string objectName;

        /** 当前分片在整个对象中的起始偏移量 */
        private long offset;

        /** 上传上下文  */
        private string context;

        /** 上传文件的输出流  */
        private Stream fileStream;

        /** 上传文件剩余大小 */
        private long remainderSize;

        /** 上传的token信息  */
        private String xNosToken;

        /** 视频上传初始化输入参数的封装类*/
        private InitUploadVideoModule initUploadVideoModule;

        /** 根据对象名查询视频或水印图片ID输入参数的封装类*/
        private QueryVideoIDorWatermarkIDModule queryVideoIDorWatermarkIDModule;

        /** 设置上传回调地址接口输入参数的封装类 */
        private SetCallbackModule setCallbackModule;

        /// <summary>
        /// 视频上传初始化 的构造函数
        /// </summary>
        /// <param name="initUploadVideoModule">视频上传初始化输入参数的封装类</param>  

        public UploadUtil(InitUploadVideoModule initUploadVideoModule)
        {
            this.initUploadVideoModule = initUploadVideoModule;
        }
        /// <summary>
        ///  获取上传加速节点地址 的构造函数
        /// </summary>
        /// <param name="bucket">桶名</param>      
        public UploadUtil(String bucket)
        {
            this.bucket = bucket;
        }

        /// <summary>
        /// 上传视频分片的构造函数
        /// </summary>
        /// <param name="bucket">存储上传文件的桶名 </param>
        /// <param name="uploadHost">上传加速节点地址</param>
        /// <param name="objectName">存储上传文件的对象名</param>
        /// <param name="offset">当前分片在整个对象中的起始偏移量</param>
        /// <param name="context"> 上传上下文</param>
        /// <param name="fileStream">上传文件的输出流</param>
        /// <param name="remainderSize">上传文件剩余大小</param>
        /// <param name="xNosToken">上传的token信息</param>    
	    public UploadUtil(string bucket, string uploadHost, string objectName,
                long offset, string context, FileStream fileStream, long remainderSize, string xNosToken)
        {
            this.bucket = bucket;
            this.uploadHost = uploadHost;
            this.objectName = objectName;
            this.offset = offset;
            this.context = context;
            this.fileStream = fileStream;
            this.remainderSize = remainderSize;
            this.xNosToken = xNosToken;
        }
        /// <summary>
        /// 上传视频分片的构造函数
        /// </summary>
        /// <param name="initUploadVideoParam">视频上传初始化返回结果的封装类</param>
        /// <param name="getUploadHostParam">获取上传加速节点地址返回结果的封装类</param>
        /// <param name="offset">当前分片在整个对象中的起始偏移量</param>
        /// <param name="context">上传上下文</param>
        /// <param name="fileStream">上传文件的输出流</param>
        /// <param name="remainderSize">上传文件剩余大小</param>       
        public UploadUtil(InitUploadVideoParam initUploadVideoParam, GetUploadHostParam getUploadHostParam, long offset, string context, Stream fileStream, long remainderSize)
        {
            this.bucket = initUploadVideoParam.ret.bucket;
            this.uploadHost = getUploadHostParam.upload[0];
            this.objectName = initUploadVideoParam.ret.objectName;
            this.offset = offset;
            this.context = context;
            this.fileStream = fileStream;
            this.remainderSize = remainderSize;
            this.xNosToken = initUploadVideoParam.ret.xNosToken;
        }

        /// <summary>
        /// 上传完成根据对象名查询视频主ID的构造函数
        /// </summary>
        /// <param name="queryVideoIDorWatermarkIDModule">根据对象名查询视频或水印图片主ID输入参数的封装类</param>     
        public UploadUtil(QueryVideoIDorWatermarkIDModule queryVideoIDorWatermarkIDModule)
        {
            this.queryVideoIDorWatermarkIDModule = queryVideoIDorWatermarkIDModule;
        }


        /// <summary>
        /// 设置上传回调地址的构造函数
        /// </summary>
        /// <param name="setCallbackModule">设置上传回调地址接口输入参数的封装类</param>
        public UploadUtil(SetCallbackModule setCallbackModule)
        {
            this.setCallbackModule = setCallbackModule;
        }

        /// <summary>
        /// 断点续传查询断点的构造函数
        /// </summary>
        /// <param name="uploadHost">上传加速节点地址</param>
        /// <param name="bucket">存储上传文件的桶名</param>
        /// <param name="objectName">存储上传文件的对象名</param>
        /// <param name="context">上传上下文</param>
        /// <param name="xNosToken">上传的token信息</param>
        public UploadUtil(string uploadHost, string bucket, string objectName, string context, string xNosToken)
        {
            this.uploadHost = uploadHost;
            this.bucket = bucket;
            this.objectName = objectName;
            this.context = context;
            this.xNosToken = xNosToken;
        }

        /// <summary>
        /// 视频上传初始化
        /// </summary>
        /// <returns>视频上传初始化返回结果的封装类</returns>
        public InitUploadVideoParam initUploadVideo()
        {
            string url = "https://vcloud.163.com/app/vod/upload/init";
            string json = JsonHelper.ToJsonString(initUploadVideoModule);
            string responseJson = HttpClientBuilder.HttpPost(url, json);
            // Console.WriteLine(responseJson);
            string responseJsonNew = responseJson.Replace("\"object\"", "\"objectName\"");
            // Console.WriteLine(responseJsonNew);
            InitUploadVideoParam initUploadVideoParam = JsonHelper.ToObject<InitUploadVideoParam>(responseJsonNew);
            return initUploadVideoParam;
        }

        /// <summary>
        /// 获取上传加速节点地址
        /// </summary>
        /// <returns>获取上传加速节点地址返回结果的封装类</returns>
        public GetUploadHostParam getUploadHost()
        {
            string url = "http://wanproxy.127.net/lbs?version=1.0&bucketname=" + this.bucket;
            string responseJson = HttpClientBuilder.HttpGet(url);
            GetUploadHostParam getUploadHostParam = JsonHelper.ToObject<GetUploadHostParam>(responseJson);
            //Console.WriteLine(getUploadHostParam.lbs);
            return getUploadHostParam;
        }

        /// <summary>
        /// 上传视频分片
        /// </summary>
        /// <returns>上传视频分片返回结果的封装类</returns>
        public UploadVideoFragmentParam uploadVideoFragment()
        {
            byte[] buffer;
            String url;
            if (remainderSize > 0)
            {

                /* 判读是否是最后一片 */
                if (remainderSize <= uploadDataSize)
                {
                    url = uploadHost + "/" + bucket + "/" + objectName + "?offset=" + offset + "&complete=" + "true" + "&version=1.0";
                    /* 如果是最后一片，申请的数组大小要根据实际情况 */
                    buffer = new byte[remainderSize];
                }
                else
                {
                    url = uploadHost + "/" + bucket + "/" + objectName + "?offset=" + offset + "&complete=" + "false" + "&version=1.0";
                    buffer = new byte[(int)uploadDataSize];
                }
                /* 如果不是第一次传输，需要加入此参数 */
                if (null != context)
                {
                    url = url + "&context=" + context;
                }
                int len = fileStream.Read(buffer, 0, buffer.Length);
                string responseJson = HttpClientBuilder.HttpPostVideo(url, buffer, xNosToken);
                UploadVideoFragmentParam uploadVideoFragmentParam = JsonHelper.ToObject<UploadVideoFragmentParam>(responseJson);
                return uploadVideoFragmentParam;
            }
            return null;
        }

        /// <summary>
        /// 上传完成根据对象名查询视频主ID
        /// </summary>
        /// <returns>查询视屏ID返回结果的封装类</returns>     
	    public QueryVideoIDorWatermarkIDParam queryVideoID()
        {

            /* 接口 */
            string url = "https://vcloud.163.com/app/vod/video/query";
            //String url = Config.getQueryVideoIDURL();		
            string json = JsonHelper.ToJsonString(this.queryVideoIDorWatermarkIDModule);
            string responseJson = HttpClientBuilder.HttpPost(url, json);
            /* 得到Json数据 */
            QueryVideoIDorWatermarkIDParam queryVideoIDParam = (QueryVideoIDorWatermarkIDParam)JsonHelper.ToObject<QueryVideoIDorWatermarkIDParam>(responseJson);

            return queryVideoIDParam;
        }


        /// <summary>
        /// 设置上传回调地址
        /// </summary>
        /// <returns>设置上传回调地址接口输出参数的封装类</returns>
        public SetCallbackParam setCallback()
        {
            /* 接口 */
            string url = "https://vcloud.163.com/app/vod/upload/setcallback";
            /* 设置请求的参数 */
            string json = JsonHelper.ToJsonString(this.setCallbackModule);
            string responseJson = HttpClientBuilder.HttpPost(url, json);
            /* 得到Json数据 */
            SetCallbackParam setCallbackParam = (SetCallbackParam)JsonHelper.ToObject<SetCallbackParam>(responseJson);

            return setCallbackParam;
        }

        /// <summary>
        /// 断点续传查询断点
        /// </summary>
        /// <returns>查询断点输出参数的封装类</returns>
        public QueryOffsetParam getPartOffset()
        {
            /* 接口 */
            string url = this.uploadHost + "/" + this.bucket + "/" + this.objectName + "?uploadContext&context=" + this.context + "&version=1.0";

            // Console.WriteLine(url);
            string responseJson = HttpClientBuilder.HttpGetWithXNosToken(url, this.xNosToken);
            /* 得到Json数据 */
            QueryOffsetParam queryOffsetParam = (QueryOffsetParam)JsonHelper.ToObject<QueryOffsetParam>(responseJson);

            return queryOffsetParam;

        }



    }
}
