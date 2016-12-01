using System.Web.Mvc;

namespace WebApp.Areas.ComManage
{
    public class ComManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ComManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ComManage_default",
                "Com/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] {"WebApp.Areas.ComManage.Controllers"}
            );
        }
    }
}