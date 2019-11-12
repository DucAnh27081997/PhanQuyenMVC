using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppEnglishTest.Areas.administrator.Models.DataModel;
using AppEnglishTest.Areas.administrator.Helper;
using AppEnglishTest.Areas.administrator.Models.BusinessModel;

namespace AppEnglishTest.Areas.adminstrator.Controllers
{
    [AuthorizeBusiness]
    public class BusinessesController : Controller
    {
        private ShopDBContext db = new ShopDBContext();

        // GET: adminstrator/Businesses
        public ActionResult Index()
        {
            return View(db.Businesses.ToList());
        }

        // GET: adminstrator/Businesses/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // GET: adminstrator/Businesses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: adminstrator/Businesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessId,BusinessName")] Business business)
        {
            if (ModelState.IsValid)
            {
                db.Businesses.Add(business);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(business);
        }

        // GET: adminstrator/Businesses/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // POST: adminstrator/Businesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessId,BusinessName")] Business business)
        {
            if (ModelState.IsValid)
            {
                db.Entry(business).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(business);
        }

        // GET: adminstrator/Businesses/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // POST: adminstrator/Businesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Business business = db.Businesses.Find(id);
            db.Businesses.Remove(business);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // Update danh sach nghiep vu
        public ActionResult UpdateBusinesses(string name)
        {
            List<Type> lstControllerTyper = ReflectionController.GetControllers("AppEnglishTest.Areas.adminstrator");
            List<string> lstOldController = db.Businesses.Select(x => x.BusinessId).ToList();
            List<string> lstOldPermission = db.Permissions.Select(x => x.PermissionName).ToList();
            foreach (var con in lstControllerTyper)
            {
                if (!lstOldController.Contains(con.Name))
                {
                    Business bs = new Business() { BusinessId = con.Name, BusinessName = "Chua co mo ta" };
                    db.Businesses.Add(bs);
                }
                List<string> lstPermisses = ReflectionController.GetActions(con);
                foreach (var p in lstPermisses)
                {
                    if (!lstOldPermission.Contains(con.Name + "-" + p))
                    {
                        Permission per = new Permission()
                        {
                            PermissionName = con.Name + "-" + p,
                            BusinessId = con.Name,
                            Description = "Chưa có mô tả về chức năng này"
                        };
                        db.Permissions.Add(per);
                    }
                }
            }
            db.SaveChanges();
            ViewBag.Error = "Update thành công";
            return RedirectToAction("Index");
        }
    }
}
