using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vCloud
{
    /// <summary>
    /// 提供给用户进行点播相关操作的视频云客户端类
    /// </summary>
    public class VcloudClient
    {
        /// <summary>
        /// 访问服务的凭据类
        /// </summary>        
        private Credentials credentials;

        /// <summary>
        /// 客户端配置类
        /// </summary>
        private ClientConfiguration clientConfiguration;

        /// <summary>
        /// 传入访问服务的凭据类和客户端配置类构造视频云客户端
        /// </summary>
        /// <param name="credentials">传入访问服务的凭据类</param>
        /// <param name="clientConfiguration">客户端配置类</param>
        ///         
        public VcloudClient(Credentials credentials,
                ClientConfiguration clientConfiguration)
        {
            this.credentials = credentials;
            this.clientConfiguration = clientConfiguration;
            init();
        }

        /// <summary>
        /// 传入访问服务的凭据类构造视频云客户端，客户端配置会采用默认配置
        /// </summary>
        /// <param name="credentials">传入访问服务的凭据类</param>
        ///     
        public VcloudClient(Credentials credentials)
        {
            this.credentials = credentials;
            this.clientConfiguration = new ClientConfiguration();
            init();
        }


        /// <summary>
        /// 利用访问服务的凭据类和客户端配置类初始化配置信息
        /// </summary>
        public void init()
        {
            Config.ConnectionTimeout = clientConfiguration.ConnectionTimeout;
            Config.AppKey = credentials.AppKey;
            Config.AppSecret = credentials.AppSecret;
        }
        /// <summary>
        /// 视频上传初始化
        /// </summary>
        /// <param name="initParamMap">上传文件的相关信息集合</param>
        /// <returns>视频上传初始化返回结果的封装类</returns>
        public InitUploadVideoParam initUploadVideo(IDictionary<String, Object> initParamMap)
        {
            InitUploadVideoModule initUploadVideoModule = new InitUploadVideoModule(initParamMap);
            UploadUtil util = new UploadUtil(initUploadVideoModule);
            return util.initUploadVideo();
        }
        /// <summary>
        /// 获取上传加速节点地址
        /// </summary>
        /// <param name="bucket">桶名</param>
        /// <returns> 获取上传加速节点地址返回结果的封装类</returns>
        public GetUploadHostParam getUploadHost(string bucket)
        {
            UploadUtil util = new UploadUtil(bucket);
            return util.getUploadHost();
        }

        /// <summary>
        /// 获取上传加速节点地址
        /// </summary>
        /// <param name="initUploadVideoParam">视频上传初始化返回结果的封装类</param>
        /// <returns>获取上传加速节点地址返回结果的封装类</returns>
        public GetUploadHostParam getUploadHost(InitUploadVideoParam initUploadVideoParam)
        {
            return this.getUploadHost(initUploadVideoParam.ret.bucket);
        }

        /// <summary>
        /// 上传视频分片
        /// </summary>
        /// <param name="initUploadVideoParam">视频上传初始化返回结果的封装类</param>
        /// <param name="getUploadHostParam">获取上传加速节点地址返回结果的封装类</param>
        /// <param name="offset">当前分片在整个对象中的起始偏移量</param>
        /// <param name="context">上传上下文</param>
        /// <param name="fileStream">上传文件的输出流</param>
        /// <param name="remainderSize">上传文件剩余大小</param>
        /// <returns>分片上传视频返回结果的封装类</returns>
        public UploadVideoFragmentParam uploadVideoFragment(InitUploadVideoParam initUploadVideoParam, GetUploadHostParam getUploadHostParam, long offset, string context, Stream fileStream, long remainderSize)
        {
            UploadUtil util = new UploadUtil(initUploadVideoParam, getUploadHostParam, offset, context, fileStream, remainderSize);
            return util.uploadVideoFragment();

        }

        /// <summary>
        /// 上传视频分片
        /// </summary>
        /// <param name="bucket">存储上传文件的桶名 </param>
        /// <param name="uploadHost">上传加速节点地址</param>
        /// <param name="objectName">存储上传文件的对象名</param>
        /// <param name="offset">当前分片在整个对象中的起始偏移量</param>
        /// <param name="context"> 上传上下文</param>
        /// <param name="fileStream">上传文件的输出流</param>
        /// <param name="remainderSize">上传文件剩余大小</param>
        /// <param name="xNosToken">上传的token信息</param>    
        /// <returns>分片上传视频返回结果的封装类</returns>
        public UploadVideoFragmentParam uploadVideoFragment(string bucket, string uploadHost, string objectName, long offset, string context, FileStream fileStream, long remainderSize, string xNosToken)
        {
            UploadUtil util = new UploadUtil(bucket, uploadHost, objectName, offset, context, fileStream, remainderSize, xNosToken);
            return util.uploadVideoFragment();
        }

        /// <summary>
        /// 上传完成后查询视频ID
        /// </summary>
        /// <param name="objectNamesList"> 查询视频的对象名集合</param>
        /// <returns>上传完成后查询视频ID返回结果的封装类</returns>
        public QueryVideoIDorWatermarkIDParam queryVideoID(List<string> objectNamesList)
        {
            QueryVideoIDorWatermarkIDModule queryVideoIDorWatermarkIDModule = new QueryVideoIDorWatermarkIDModule(objectNamesList);
            UploadUtil util = new UploadUtil(queryVideoIDorWatermarkIDModule);
            return util.queryVideoID();
        }

        /// <summary>
        /// 简单视频上传
        /// </summary>
        /// <param name="filePath">上传文件路径</param>
        /// <param name="initParamMap">上传文件的相关信息集合</param>
        /// <returns>上传完成后查询视频ID返回结果的封装类</returns>
        public QueryVideoIDorWatermarkIDParam uploadVideo(string filePath, IDictionary<String, Object> initParamMap)
        {
            if (!FileUtil.doesFileExist(filePath))
            {
                throw new VcloudException(string.Format("{0} does not exist", filePath));
            }
            /*视频上传初始化*/
            /*视频上传初始化返回结果的封装类*/
            InitUploadVideoParam initUploadVideoParam = initUploadVideo(initParamMap);

            if (initUploadVideoParam.code != 200)
            {
                throw new VcloudException("[VcloudClient]上传初始化失败 return code : " + initUploadVideoParam.code + " return message: " + initUploadVideoParam.msg);
            }
            /*获取上传加速节点地址*/
            /*获取上传加速节点地址返回结果的封装类*/
            GetUploadHostParam getUploadHostParam = getUploadHost(initUploadVideoParam);
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
                /*上传文件的输出流 */
                FileStream fileStream = null;

                fileStream = FileUtil.getFileInputStream(filePath);
                /*上传文件剩余大小*/
                long fileLength = FileUtil.getFileLength(filePath);
                long remainderSize = fileLength;
                /*分片上传视频*/
                while (remainderSize > 0)
                {
                    UploadVideoFragmentParam uploadVideoParam = uploadVideoFragment(initUploadVideoParam, getUploadHostParam, offset, context, fileStream, remainderSize);

                    if (null == uploadVideoParam.errMsg || "".Equals(uploadVideoParam.errMsg.Trim()))
                    {
                        context = uploadVideoParam.context;
                        offset = uploadVideoParam.offset;
                        remainderSize = fileLength - offset;
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
                    QueryVideoIDorWatermarkIDParam queryVideoIDParam = queryVideoID(objectNamesList);
                    return queryVideoIDParam;
                }
                else
                {
                    throw new VcloudException("文件未传输完成");
                }

            }
            catch (Exception e)
            {
                throw new VcloudException("[VcloudClient]上传视频失败。 error message: " + e.Message);
            }
        }
        /// <summary>
        /// 支持断点续传的视频上传
        /// </summary>
        /// <param name="filePath">上传文件路径</param>
        /// <param name="initParamMap">上传文件的相关信息集合</param>
        /// <param name="recorder">断点续传时记录上传进度的对象</param>
        /// <returns>上传完成则返回查询视频ID返回结果的封装类，否则抛出文件未传输完成异常</returns>
        public QueryVideoIDorWatermarkIDParam uploadVideoWithRecorder(string filePath, IDictionary<String, Object> initParamMap, UploadRecorder recorder)
        {
            /*  第一次上传 */
            if (!recorder.uploadAgain)
            {
                if (!FileUtil.doesFileExist(filePath))
                {
                    throw new VcloudException("文件无法访问");
                }
                else
                {
                    return uploadVideoFirstWithRecorder(filePath, initParamMap, recorder);
                }

            }
            /* 断点续传 */
            else
            {
                return uploadVideoAgainWithRecorder(filePath, initParamMap, recorder);
            }

        }

        /// <summary>
        /// 文件第一次上传
        /// </summary>
        /// <param name="filePath">上传文件路径</param>
        /// <param name="initParamMap">上传文件的相关信息集合</param>
        /// <param name="recorder">断点续传时记录上传进度的对象</param>
        /// <returns>上传完成则返回查询视频ID返回结果的封装类，否则抛出文件未传输完成异常</returns>
        public QueryVideoIDorWatermarkIDParam uploadVideoFirstWithRecorder(string filePath, IDictionary<String, Object> initParamMap, UploadRecorder recorder)
        {

            /*视频上传初始化*/
            /*视频上传初始化返回结果的封装类*/
            InitUploadVideoParam initUploadVideoParam = initUploadVideo(initParamMap);
            if (initUploadVideoParam.code != 200)
            {
                throw new VcloudException("[VcloudClient]上传初始化失败 return code : " + initUploadVideoParam.code + " return message: " + initUploadVideoParam.msg);
            }
            /*获取上传加速节点地址*/
            /*获取上传加速节点地址返回结果的封装类*/
            GetUploadHostParam getUploadHostParam = getUploadHost(initUploadVideoParam);
            if (null == getUploadHostParam || null == getUploadHostParam.upload || getUploadHostParam.upload.Count == 0)
            {
                throw new VcloudException("[VcloudClient]获取上传加速节点地址失败 return code: " + getUploadHostParam.code + " return message: " + getUploadHostParam.Message);
            }
            /*分片上传视频*/
            /*当前分片在整个对象中的起始偏移量 */
            long offset = 0;
            /*上传上下文*/
            string context = null;
            /*上传文件的输出流 */
            FileStream fileStream = null;

            //int count = 0;
            try
            {
                fileStream = FileUtil.getFileInputStream(filePath);
                /*上传文件剩余大小*/
                long fileLength = FileUtil.getFileLength(filePath);
                long remainderSize = fileLength;
                /*分片上传视频*/
                while (remainderSize > 0)
                {

                    UploadVideoFragmentParam uploadVideoParam = uploadVideoFragment(initUploadVideoParam, getUploadHostParam, offset, context, fileStream, remainderSize);
                    if (null == uploadVideoParam.errMsg || "".Equals(uploadVideoParam.errMsg.Trim()))
                    {
                        context = uploadVideoParam.context;
                        offset = uploadVideoParam.offset;
                        remainderSize = fileLength - offset;

                        string bucket = initUploadVideoParam.ret.bucket;
                        string uploadHost = getUploadHostParam.upload[0];
                        string objectName = initUploadVideoParam.ret.objectName;
                        string offsetStr = Convert.ToString(offset);
                        // string context = saveInfo[4];
                        string remainderSizeStr = Convert.ToString(remainderSize);
                        string xNosToken = initUploadVideoParam.ret.xNosToken;
                        string[] saveInfo = new string[] { bucket, uploadHost, objectName, offsetStr, context, remainderSizeStr, xNosToken };
                        recorder.saveRecorderInfo(saveInfo);

                        /*
                        count++;
                        if (count == 1)
                        {
                            Console.WriteLine("******************************第一次强制中断************");
                            break;                           
                        }*/
                    }
                    else
                    {
                        throw new VcloudException("upload videoFragment fail. errMsg: " + uploadVideoParam.errMsg + " requestID: " + uploadVideoParam.requestId);
                    }

                }
                if (remainderSize == 0)
                {
                    recorder.deleteRecorder();
                    /* 查询上传视频的vid*/
                    List<string> objectNamesList = new List<string>();
                    objectNamesList.Add(initUploadVideoParam.ret.objectName);

                    /*查询上传视屏返回结果的封装类*/
                    QueryVideoIDorWatermarkIDParam queryVideoIDParam = queryVideoID(objectNamesList);
                    return queryVideoIDParam;
                }
                else
                {
                    throw new VcloudException("文件未传输完成");
                    //return null;
                }

            }
            catch (Exception e)
            {
                throw new VcloudException(e.Message);
            }

        }

        /// <summary>
        /// 文件续传
        /// </summary>
        /// <param name="filePath">上传文件路径</param>
        /// <param name="initParamMap">上传文件的相关信息集合</param>
        /// <param name="recorder">断点续传时记录上传进度的对象</param>
        /// <returns>上传完成则返回查询视频ID返回结果的封装类，否则抛出文件未传输完成异常</returns>
        public QueryVideoIDorWatermarkIDParam uploadVideoAgainWithRecorder(string filePath, IDictionary<String, Object> initParamMap, UploadRecorder recorder)
        {


            string[] savedInfo = recorder.getRecorderInfo();

            string bucket = savedInfo[1];
            string uploadHost = savedInfo[2];
            string objectName = savedInfo[3];
            long offset = long.Parse(savedInfo[4]);
            string context = savedInfo[5];
            long remainderSize = long.Parse(savedInfo[6]);
            string xNosToken = savedInfo[7];

            /*分片上传视频*/
            FileStream fileStream = null;

            try
            {
                fileStream = FileUtil.getFileInputStream(filePath);
                fileStream.Seek(offset, SeekOrigin.Begin);
                /*上传文件剩余大小*/
                long fileLength = FileUtil.getFileLength(filePath);
                /*分片上传视频*/
                while (remainderSize > 0)
                {
                    UploadVideoFragmentParam uploadVideoParam = uploadVideoFragment(bucket, uploadHost, objectName, offset, context, fileStream, remainderSize, xNosToken);
                    if (null == uploadVideoParam.errMsg || "".Equals(uploadVideoParam.errMsg.Trim()))
                    {
                        context = uploadVideoParam.context;
                        offset = uploadVideoParam.offset;
                        remainderSize = fileLength - offset;

                        // string bucket = initUploadVideoParam.ret.bucket;
                        // string uploadHost = getUploadHostParam.upload[0];
                        // string objectName = initUploadVideoParam.ret.objectName;
                        string offsetStr = Convert.ToString(offset);
                        // string context = saveInfo[4];
                        string remainderSizeStr = Convert.ToString(remainderSize);
                        // string xNosToken = initUploadVideoParam.ret.xNosToken;
                        string[] saveInfo = new string[] { bucket, uploadHost, objectName, offsetStr, context, remainderSizeStr, xNosToken };
                        recorder.saveRecorderInfo(saveInfo);
                    }
                    else
                    {
                        throw new VcloudException("upload videoFragment fail. errMsg: " + uploadVideoParam.errMsg + " requestID: " + uploadVideoParam.requestId);
                    }

                }
                if (remainderSize == 0)
                {
                    recorder.deleteRecorder();
                    /* 查询上传视频的vid*/
                    List<string> objectNamesList = new List<string>();
                    objectNamesList.Add(objectName);

                    /*查询上传视屏返回结果的封装类*/
                    QueryVideoIDorWatermarkIDParam queryVideoIDParam = queryVideoID(objectNamesList);
                    return queryVideoIDParam;
                }
                else
                {
                    throw new VcloudException("文件未传输完成");
                }

            }
            catch (Exception e)
            {
                throw new VcloudException(e.Message);
            }
        }

        /// <summary>
        /// 设置上传回调地址
        /// </summary>
        /// <param name="callbackUrl">上传成功后回调客户端的URL地址</param>
        /// <returns></returns>
        public SetCallbackParam setCallback(string callbackUrl)
        {
            SetCallbackModule setCallbackModule = new SetCallbackModule(callbackUrl);

            UploadUtil util = new UploadUtil(setCallbackModule);

            return util.setCallback();
        }

        /// <summary>
        /// 断点续传查询断点
        /// </summary>
        /// <param name="uploadHost">上传加速节点地址</param>
        /// <param name="bucket">存储上传文件的桶名</param>
        /// <param name="objectName">存储上传文件的对象名</param>
        /// <param name="context">上传上下文</param>
        /// <param name="xNosToken">上传的token信息</param>
        /// <returns></returns>
        public QueryOffsetParam getPartOffset(string uploadHost, string bucket, string objectName, string context, string xNosToken)
        {
            UploadUtil util = new UploadUtil(uploadHost, bucket, objectName, context, xNosToken);
            return util.getPartOffset();
        }
    }
}
