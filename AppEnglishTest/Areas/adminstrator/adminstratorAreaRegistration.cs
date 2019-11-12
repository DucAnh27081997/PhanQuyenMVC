using System.Web.Mvc;

namespace AppEnglishTest.Areas.adminstrator
{
    public class adminstratorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "adminstrator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "adminstrator_default",
                "admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "AppEnglishTest.Areas.adminstrator.Controllers" }
            );
        }
    }
}