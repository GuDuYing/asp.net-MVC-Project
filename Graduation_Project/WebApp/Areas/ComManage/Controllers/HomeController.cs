using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Areas.ComManage.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 网站主页
        /// </summary>
        /// <returns></returns>
        // GET: ComManage/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}