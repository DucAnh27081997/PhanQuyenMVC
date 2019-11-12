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
using AppEnglishTest.Areas.administrator.Helper;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;

namespace AppEnglishTest.Areas.adminstrator.Controllers
{
    [AuthorizeBusiness]
    public class AccountsController : Controller
    {
        private ShopDBContext db = new ShopDBContext();

        // GET: adminstrator/Accounts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getList()
        {
            return Json(db.Accounts.ToList(), JsonRequestBehavior.AllowGet);
        }
        // GET: adminstrator/Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: adminstrator/Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: adminstrator/Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,FullName,Password,Salt,Avatar,isAdmin,Allowed,PhoneNumber,Email,Checked")] Account account)
        {
            if (ModelState.IsValid)
            {

                account.Password = security.HashPass(account.Password,account.UserName);
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        [HttpPost]
        public ActionResult CreateUser(Account account)
        {
            var listUsr = db.Accounts.ToList();
            foreach(Account u in listUsr)
            {
                if (account.UserName == u.UserName)
                {
                     return Json(new { Result = "Error", Message = "User Đã Tồn Tại" }, JsonRequestBehavior.AllowGet);
                }
            }

            account.Password  = security.HashPass(account.Password, account.UserName);
            db.Accounts.Add(account);
            db.SaveChanges();
            return Json(new { Result = "Success", Message = "Tạo User Thành Công" }, JsonRequestBehavior.AllowGet);
        }

        // GET: adminstrator/Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: adminstrator/Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,FullName,Password,Salt,Avatar,isAdmin,Allowed,PhoneNumber,Email,Checked")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: adminstrator/Accounts/Delete/5

        [HttpGet]
        public ActionResult DeleteUser(int? id)
        {
            try
            {
                if (id == null)
                {
                    return Json(new { Result = "Error", Message = "Bạn chưa chọn ID user!" }, JsonRequestBehavior.AllowGet);
                }
                Account account = db.Accounts.Find(id);
                if(account.isAdmin == 1)
                {
                    return Json(new { Result = "Error", Message = "Dây là TK người quản trị không thể xóa!" }, JsonRequestBehavior.AllowGet);
                }
                if (account == null)
                {
                    return Json(new { Result = "Error", Message = "User không tồn!" }, JsonRequestBehavior.AllowGet);
                }
                db.Accounts.Remove(account);
                db.SaveChanges();
                return Json(new { Result = "Success", Message = "Xóa User thành công " + account.UserName + "Đã bị xóa khỏi hệ thống" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Result = "Error", Message = e.Message }, JsonRequestBehavior.AllowGet);
            }
           
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }
        // POST: adminstrator/Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return Json(new { Result = "Error", Message =" Error not found Account " }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                db.Accounts.Remove(account);
                db.SaveChanges();
                return Json(new { Result = "Success", Message = "Update success" }, JsonRequestBehavior.AllowGet);
            }
           
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Update mật khẩu
        [HttpPost]
        public ActionResult UpdatePassWord(string newPass,string oldPass)
        {
            var pass= (int)(Session["userid"]);
            if (pass != null)
            {
                var user = db.Accounts.Where(x => x.UserId == pass && x.Allowed == 1).FirstOrDefault(); 
                user.Password = administrator.Helper.security.HashPass(newPass,user.UserName);
                db.SaveChanges();
                return Json(new { Result = "Success", Message = "Update Password Success "}, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Result = "Error", Message = " Error not found Account " }, JsonRequestBehavior.AllowGet);
        }

        // khich hoat tk
        public ActionResult ActiveAccount(int id)
        {
            try
            {
                var user = db.Accounts.Where(x => x.UserId == id).FirstOrDefault();
                if (user != null)
                {
                    if (user.Allowed == 1)
                    {
                        user.Allowed = 0;
                        db.SaveChanges();
                        return Json(new { Result = "Success", active = 0 }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        user.Allowed = 1;
                        db.SaveChanges();
                        return Json(new { Result = "Success", active = 1 }, JsonRequestBehavior.AllowGet);

                    }
                }
                return Json(new { Result = "Error", message = "Ko tìm thấy User" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new { Result = "Success", message = e.Message }, JsonRequestBehavior.AllowGet);
            }
           
        }

        //public ActionResult Export(string ReportType)
        //{
        //    LocalReport localReport = new LocalReport();
        //    @Session["error"] = null;
        //    string url = Server.MapPath("~/");
        //    localReport.ReportPath = Server.MapPath(@"~/Areas/adminstrator/Report/ReportUser.rdlc");
        //    try
        //    {
        //        ReportDataSource RDC = new ReportDataSource();
        //        RDC.Name = "DataSet1";
    
        //        RDC.Value = db.Accounts.ToList();

        //        localReport.DataSources.Add(RDC);
        //        string reportType = ReportType;
        //        string mineType;
        //        string encoding;
        //        string fileNameExtention;
        //        if (reportType == "Excel")
        //        {
        //            fileNameExtention = "xlsx";
        //        }
        //        else if (reportType == "Word")
        //        {
        //            fileNameExtention = "docx";
        //        }
        //        else if (reportType == "PDF")
        //        {
        //            fileNameExtention = "pdf";
        //        }
        //        string[] stream;
        //        Warning[] warning;
        //        byte[] renderedbyte;
        //        renderedbyte = localReport.Render(reportType, "", out mineType, out encoding, out fileNameExtention, out stream, out warning);

        //        Response.AddHeader("content-disposition", "attachment:filename=BaoCao." + fileNameExtention);
        //        return File(renderedbyte, fileNameExtention);
        //    }
        //    catch(Exception e)
        //    {
        //        Session["error"] = e.Message;
        //        return RedirectToAction("Index");
        //    }
          
        //}

        public void Export2()
        {
            List<Account> emplist = db.Accounts.ToList();
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells.Style.Font.Size = 11;
            // font family mặc định cho cả sheet
            ws.Cells.Style.Font.Name = "Calibri";

            ws.Cells["A1"].Value = "Communication";
            ws.Cells["B1"].Value = "Com1";
            ws.Cells["A2"].Value = "Report";
            ws.Cells["B2"].Value = "Report1";
            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "ID";
            ws.Cells["B6"].Value = "FullName";
            ws.Cells["C6"].Value = "Email";
            ws.Cells["D6"].Value = "isAdmin";
            ws.Cells["E6"].Value = "Phone";

            int rowStart = 7;
            foreach (var item in emplist)
            {
                if (item.isAdmin == 1)
                {
                    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                    ws.Row(rowStart).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                }
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.UserId;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.FullName;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Email;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.isAdmin;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.PhoneNumber;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }

        //[HttpGet]
        //public ActionResult UpdatePassword(string newPass)
        //{
        //    try
        //    {
        //        var user = db.Accounts.Where(x => x.UserId == (int)Session["userid"]).FirstOrDefault();
        //        if (user != null)
        //        {
                    
        //                user.Password = security.HashPass(newPass,user.UserName);
        //                db.SaveChanges();
        //                return Json(new { Result = "Success", Message = "Pass đã được cập nhật" }, JsonRequestBehavior.AllowGet);

        //        }
        //        return Json(new { Result = "Error", Message = "Ko tìm thấy User" }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new { Result = "Error", Message = e.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}
