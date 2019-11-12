using AppEnglishTest.Areas.administrator.Models.BusinessModel;
using AppEnglishTest.Areas.administrator.Models.DataModel;
using AppEnglishTest.Areas.adminstrator.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppEnglishTest.Areas.adminstrator.Controllers
{
    [AuthorizeBusiness]
    public class GantPermissionController : Controller
    {
        ShopDBContext db = new ShopDBContext();
        // GET: Administrator/GantPermission
        public ActionResult Index(int id)
        {
            // danh sach cac controller
            var lstController = db.Businesses.AsEnumerable();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in lstController)
            {
                items.Add(new SelectListItem() { Value = item.BusinessId, Text = item.BusinessName });
            }
            ViewBag.items = items;

            // danh sach cac quyen cua user da dc cap
            var lstGanted = from g in db.GantPermissions
                            join p in db.Permissions on g.PermissionId equals p.PermissionId
                            where g.UserId == id
                            select new SelectListItem() { Value = g.PermissionId.ToString(), Text = g.Description };


            ViewBag.lstGanted = lstGanted;
            // lấy thông tin về User
            Session["UserGant"] = id;
            Account user = db.Accounts.Find(id);
            ViewBag.UserGant = user.UserName + "(" + user.FullName + ")";
            return View();
        }

        public JsonResult getPermissions(string id, int usertemp)
        {
            // lấy tất cả các action thuoc controller của User dang muốn cấp
            var listGranted = (from g in db.GantPermissions
                               join p in db.Permissions
                               on g.PermissionId equals p.PermissionId
                               where g.UserId == usertemp && p.BusinessId == id
                               select new PermissionAction
                               {
                                   isGranted = true,
                                   PermissionId = p.PermissionId,
                                   PermissionName = p.PermissionName,
                                   Description = p.Description
                               }).ToList();



            // lay tat cac cac action cua Controller đó
            var listActionController = (from p in db.Permissions
                                        where p.BusinessId == id
                                        select new PermissionAction
                                        {
                                            isGranted = false,
                                            PermissionId = p.PermissionId,
                                            PermissionName = p.PermissionName,
                                            Description = p.Description
                                        }).ToList();

            // lay id cua cac controlle 
            var listIDActionController = listGranted.Select(p => p.PermissionId);

            foreach (var item in listActionController)
            {
                if (!listIDActionController.Contains(item.PermissionId))
                {
                    listGranted.Add(item);
                }
            }

            return Json(listGranted.OrderBy(x => x.Description), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult updatePermission(int id, int usertemp)
        {
            try
            {
                var grant = db.GantPermissions.Find(id, usertemp);
                if (grant == null)
                {
                    db.GantPermissions.Add(new GantPermission()
                    {
                        UserId = usertemp,
                        PermissionId = id,
                        Description = "",

                    });
                    db.SaveChanges();
                    return Json(new { Result = "success", Message = "Thêm quyền thành công!" },JsonRequestBehavior.AllowGet); 
                }
                else
                {
                    db.GantPermissions.Remove(grant);
                    db.SaveChanges();
                    return Json(new { Result = "error", Message = "Đã xóa quyền thanh công!" },JsonRequestBehavior.AllowGet);

                }
                
            }
            catch(Exception e)
            {
                return Json(new { Result = "error", Message = e.Message }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}