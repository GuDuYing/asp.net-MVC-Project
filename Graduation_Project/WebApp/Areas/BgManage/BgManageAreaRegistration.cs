using System.Web.Mvc;

namespace WebApp.Areas.BgManage
{
    public class BgManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BgManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BgManage_default",
                "Bg/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "WebApp.Areas.BgManage.Controllers" }
            );
        }
    }
}