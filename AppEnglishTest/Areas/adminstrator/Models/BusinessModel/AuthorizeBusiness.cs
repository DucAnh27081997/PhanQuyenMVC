using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AppEnglishTest.Areas.administrator.Models.DataModel;

namespace AppEnglishTest.Areas.administrator.Models.BusinessModel
{
    public class AuthorizeBusiness : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (HttpContext.Current.Session["userid"] == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Home/Login");
                return;
            }
            // lay quyen trong co so du lieu
            string nameAction = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "Controller-" + filterContext.ActionDescriptor.ActionName;
            int userID = (int.Parse(HttpContext.Current.Session["userid"].ToString()));

            ShopDBContext DB = new ShopDBContext();
            var user = DB.Accounts.Where(x => x.UserId == userID && x.Allowed == 1).FirstOrDefault();
            if (user !=null)
            {
                if (user.isAdmin == 1)
                {
                    return;
                }
                else
                {
                    
                    var listMission = from p in DB.Permissions
                                      join g in DB.GantPermissions
                                      on p.PermissionId equals g.PermissionId
                                      where g.UserId == userID
                                      select p.PermissionName;

                    if (!listMission.Contains(nameAction))
                    {
                        filterContext.Result = new RedirectResult("~/Admin/Home/NotificationAuthorize");
                    }
                }
            }          
        }
    }
}