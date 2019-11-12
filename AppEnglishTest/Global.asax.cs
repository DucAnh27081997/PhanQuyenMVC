using AppEnglishTest.Areas.administrator.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AppEnglishTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // khởi tạo CSDL nếu có sự thay đổi trong model

            Database.SetInitializer(new ShopDataBaseInitializer());
        }
        protected void Session_Start()
        {
            Session["userid"] = null;
            Session["userName"] = null;
            Session["fullName"] = null;
            Session["avatar"] = null;
        }
    }
}
