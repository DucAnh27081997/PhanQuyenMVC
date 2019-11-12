using AppEnglishTest.Areas.administrator.Models.BusinessModel;
using AppEnglishTest.Areas.administrator.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace AppEnglishTest.Areas.adminstrator.Controllers
{
    [AuthorizeBusiness]
    public class SecurityController : Controller
    {
        private ShopDBContext db = new ShopDBContext();      
        // GET: adminstrator/Accounts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetListHistoryAccess()
        {
            return Json(db.UserLogss.ToList(), JsonRequestBehavior.AllowGet);       
        }
        public ActionResult GetListHistoryAccessbyID(int? id )
        {
            return Json(db.UserLogss.Where(x=>x.IDUser == id).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}
