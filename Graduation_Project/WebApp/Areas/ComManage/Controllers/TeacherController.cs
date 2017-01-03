using Commons;
using IService.VideoManage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Areas.ComManage.Controllers
{
    /// <summary>
    /// 教师控制器
    /// </summary>
    public class TeacherController : Controller
    {
        #region 声明容器
        IVideoService VideoManage { get; set; }

        #endregion
        // GET: ComManage/Teacher
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadVideo()
        {
            return View();
        }


        [HttpPost]
        public ActionResult VideoUpload()
        {
            
            string clientIP = WebHelper.GetHostAddress();
            //if(!WebHelper.IsIPAddress(clientIP))
            //{
            //    return Json("客户端ip异常");
            //}
            try
            {
                foreach (string f in Request.Files)
                {
                    
                    HttpPostedFileBase file = Request.Files[f];
                    
                    if(file != null && file.ContentLength > 0)
                    {
                        
                        string filename = file.FileName;
                        long maxres = file.InputStream.Length;
                        Stream data = file.InputStream;
                        string videoid = VideoManage.UploadVideo(clientIP, filename, data, maxres);
                        if(videoid != null && videoid != "")
                        {
                            return Json("上传成功" + videoid);
                        }
                        else
                        {
                            return Json("上传失败");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
                throw e;
            }
            return Json("ERROR");
        }
        


    }
}