using Commons;
using Commons.JsonHelper;
using Commons.Log;
using IService.ComManage;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Areas.ComManage.Controllers
{
    public class UserController : Controller
    {
        #region 声明容器
        //用户管理
        IUserService userManage { get; set; }
        //日志管理
        IExtLog log = ExtLogManager.GetLogger("T_LOG");
        #endregion
        // GET: ComManage/User

        #region 基本控制器
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 请求用户登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        //[ValidateAntiForgeryToken]
        public ActionResult UserLogin(string email,string pwd)
        {
            var json = new JsonHelper() { Msg = "", Status = "" };
            try
            {
                var user = userManage.UserLogin(email, pwd);
                if(user != null)
                {
                    if(user.UserStatu == "1")
                    {
                        json.Msg = "用户状态已经被锁定，请联系管理员进行解锁";
                        log.Warn(Utils.GetIP(), user.UserEmail, Request.Url.ToString(), "Login", "用户登录，结果为：" + json.Msg);
                        return Json(json);
                    }
                    json.Msg = "登录成功";
                    json.Status = "Y";
                    log.Info(Utils.GetIP(), user.UserEmail, Request.Url.ToString(), "Login", "用户登录，结果为：" + json.Msg);
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    json.Msg = "密码错误或者用户名错误";
                    log.Error(Utils.GetIP(), email, Request.Url.ToString(), "Login", "用户登录，结果为：" + json.Msg);
                    return Json(json);
                }
            }
            catch (Exception e)
            {
                json.Msg = e.Message;
                log.Error(Utils.GetIP(), email, Request.Url.ToString(), "Login", "用户登录，结果为：" + json.Msg);

            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Register()
        {
            return View();
        }

        public ActionResult UserRegister(string email,string username,string password,string phone)
        {
            T_User user = new T_User();
            user.UserEmail = email;
            user.UserName = username;
            user.UserPwd = password;
            user.UsrPhone = phone;
            var json = new JsonHelper() { Msg = "", Status = "" };
            try
            {
                string name = userManage.UserRegister(user);
                if (name != null)
                {
                    json.Msg = "注册成功";
                    json.Status = "Y";
                    log.Info(Utils.GetIP(), user.UserEmail, Request.Url.ToString(), "Login", "用户注册，结果为：" + json.Msg);
                }
                else
                {
                    json.Msg = "注册失败";
                    log.Error(Utils.GetIP(), email, Request.Url.ToString(), "Login", "用户注册，结果为：" + json.Msg);
                    return Json(json);
                }
            }
            catch (Exception e)
            {
                json.Msg = e.InnerException.ToString();
                log.Error(Utils.GetIP(), email, Request.Url.ToString(), "Login", "用户注册，结果为：" + json.Msg);

            }
            return Json(user.UID.ToString() +","+ json.Msg);
            return Json(json, JsonRequestBehavior.AllowGet);
        }






        #endregion



    }
}