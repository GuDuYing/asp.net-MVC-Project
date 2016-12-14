using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace vCloud
{
    /// <summary>
    /// 断点续传时记录上传进度的实现类
    /// </summary>
    public class UploadRecorder
    {
        /** 本地存储上传进度相关信息的文件路径*/
        private string recordFilePath;

        /* 标记文件是否是续传，如果文件是续传，则为true，否则为false*/
        public bool uploadAgain { get; set; } 	

        public UploadRecorder(string recordFilePath) 
        {
            this.recordFilePath = recordFilePath;
            /** 当文件并不存在，则创建 */
            if (!System.IO.File.Exists(recordFilePath))
            {
                FileInfo file = new FileInfo(recordFilePath);
                //取得目录
                DirectoryInfo directory = file.Directory;               
                if (!directory.Exists)
                {
                    directory.Create();
                }
                file.Create();
                this.uploadAgain = false;
            }else{ //文件存在则需要判断此文件中是否包含上传进度信息			
                this.uploadAgain = isUploadAgain();
            }       
        }
     
        /// <summary>
        ///  新建或更新文件分片上传的进度相关信息
        /// </summary>
        /// <param name="saveInfo">进度信息</param>
        public void saveRecorderInfo(string[] saveInfo)
        {
            try
            {
                string bucket = saveInfo[0];
                string uploadHost = saveInfo[1];
                string objectName = saveInfo[2];
                string offset = saveInfo[3];
                string context = saveInfo[4];
                string remainderSize = saveInfo[5];
                string xNosToken = saveInfo[6];

                string[] newSaveInfo = new string[] { "uploadAgain:true", bucket, uploadHost, objectName, offset, context, remainderSize, xNosToken };
                File.WriteAllLines(recordFilePath, newSaveInfo, Encoding.UTF8);
            }
            catch (Exception e)
            {
                throw new VcloudException("[UploadRecorder] fail to save recorder info. " + e.Message);
            }         
 
        
        }

    
        /// <summary>
        /// 获取文件分片上传的进度相关信息，以便可以进行断点续传
        /// </summary>
        /// <returns>进度信息</returns>
        public string[] getRecorderInfo()
        {
            try
            {                
                string[] saveInfo = File.ReadAllLines(recordFilePath);
                if (!checkRecorderInfo(saveInfo))
                {
                    throw new VcloudException("[UploadRecorder] 断点续传的进度文件已损坏. ");
                }
                return saveInfo;
            }
            catch (Exception e)
            {
                throw new VcloudException("[UploadRecorder] fail to get recorder info. " + e.Message);
            }    
        
        }

        /// <summary>
        /// 检查断点续传的进度文件是否已损坏
        /// </summary>
        /// <param name="saveInfo">从文件中读取出的数据</param>
        /// <returns>如果有参数为null，意味着对应信息缺失，则返回false，否则返回true。如果用户恶意修改数据，无法检查是否修改</returns>
        public bool checkRecorderInfo(string[] saveInfo)
        {
            if (null == saveInfo || saveInfo.Length != 8)
                return false;
            return true;    
        
        }

        /// <summary>
        /// 判断已经存在的文件是否包含上传进度相关信息
        /// </summary>
        /// <returns>如果包含上传进度相关信息，则返回true，否则返回false。</returns>
        public bool isUploadAgain()
        {
            string[] strs = File.ReadAllLines(recordFilePath);

            /* 保存的上传进度相关信息第一行均是 "uploadAgain:true" */
            if(null != strs && strs.Length != 0 && "uploadAgain:true".Equals(strs[0]))             
                return true;
            else
                return false;
        }

        /// <summary>
        /// 文件上传完成后，将删除记录断点续传的进度文件
        /// </summary>
        public void deleteRecorder()
        {
            File.Delete(this.recordFilePath);
        }
	
    }

}
