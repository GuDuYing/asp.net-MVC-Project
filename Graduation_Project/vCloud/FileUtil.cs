using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace vCloud
{
    /// <summary>
    /// 文件相关操作的工具类
    /// </summary>
    public class FileUtil
    {
        /// <summary>
        /// 根据文件路径判断文件是否存在
        /// </summary>
        /// <param name="filePath">上传文件的路径</param>
        /// <returns> 如果文件存在返回true，否则抛异常</returns>
        public static bool doesFileExist(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
			    return true;
		    }else{                
			    throw new VcloudException("[FileUtil] the file does not exit");
		    }
	    }
        /// <summary>
        /// 根据文件路径得到上传文件的大小
        /// </summary>
        /// <param name="filePath">上传文件的路径</param>
        /// <returns>如果文件存在返回该文件的大小，否则返回0</returns>
        public static long getFileLength(String filePath) 
        {
            try 
            {
                if (doesFileExist(filePath))
                {
                    FileInfo file = new FileInfo(filePath);
                    return file.Length;
                }
            }
            catch (VcloudException e)
            {
                throw new VcloudException(e.Message);
            }		  
		    return 0;
	    }

        /// <summary>
        /// 根据文件路径得到上传文件的输出流
        /// </summary>
        /// <param name="filePath">上传文件的路径</param>
        /// <returns>如果文件存在返回该文件的输出流，否则返回null</returns>
        public static FileStream getFileInputStream(String filePath) 
        {
            try
            {
                if (doesFileExist(filePath))
                {
                    return new FileStream(filePath, FileMode.Open);
                }
            }
            catch (VcloudException e)
            {
                throw new VcloudException(e.Message);
            }		    
		    return null;
    	}

        /// <summary>
        ///  根据文件路径取得文件名
        /// </summary>
        /// <param name="filePath">上传文件的路径</param>
        /// <returns>如果文件存在返回该文件的文件名（带后缀），否则返回null</returns>
        public static String getFileName(String filePath)
        {
            try
            {
                if (doesFileExist(filePath))
                {
                    return Path.GetFileName(filePath);
                }
            }
            catch (VcloudException e)
            {
                throw new VcloudException(e.Message);
            }		    
		    return null;
    	}
    }
}
