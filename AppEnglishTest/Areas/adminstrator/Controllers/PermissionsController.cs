using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppEnglishTest.Areas.administrator.Models.DataModel;
using AppEnglishTest.Areas.administrator.Models.BusinessModel;

namespace AppEnglishTest.Areas.adminstrator.Controllers
{
    [AuthorizeBusiness]
    public class PermissionsController : Controller
    {
        private ShopDBContext db = new ShopDBContext();

        // GET: adminstrator/Permissions
        public ActionResult Index(string id)
        {
            var permissions = db.Permissions.Where(x => x.BusinessId == id);
            return View(permissions.ToList());
        }

        // GET: adminstrator/Permissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permission permission = db.Permissions.Find(id);
            if (permission == null)
            {
                return HttpNotFound();
            }
            return View(permission);
        }

        // GET: adminstrator/Permissions/Create
        public ActionResult Create()
        {
            ViewBag.BusinessId = new SelectList(db.Businesses, "BusinessId", "BusinessName");
            return View();
        }

        // POST: adminstrator/Permissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PermissionId,PermissionName,Description,BusinessId")] Permission permission)
        {
            if (ModelState.IsValid)
            {
                db.Permissions.Add(permission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessId = new SelectList(db.Businesses, "BusinessId", "BusinessName", permission.BusinessId);
            return View(permission);
        }

        // GET: adminstrator/Permissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permission permission = db.Permissions.Find(id);
            if (permission == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessId = new SelectList(db.Businesses, "BusinessId", "BusinessName", permission.BusinessId);
            return View(permission);
        }

        // POST: adminstrator/Permissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PermissionId,PermissionName,Description,BusinessId")] Permission permission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new
                {
                    id = Session["namePermission"],
                });
            }
            ViewBag.BusinessId = new SelectList(db.Businesses, "BusinessId", "BusinessName", permission.BusinessId);
            return View(permission);
        }

        // GET: adminstrator/Permissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permission permission = db.Permissions.Find(id);
            if (permission == null)
            {
                return HttpNotFound();
            }
            return View(permission);
        }

        // POST: adminstrator/Permissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Permission permission = db.Permissions.Find(id);
            db.Permissions.Remove(permission);
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
    }
}
