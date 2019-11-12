using AppEnglishTest.Areas.administrator.Helper;
using AppEnglishTest.Areas.administrator.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.IO;
using AppEnglishTest.Areas.administrator.Models.BusinessModel;

namespace AppEnglishTest.Areas.adminstrator.Controllers
{
    public class HomeController : Controller
    {
        ShopDBContext Db = new ShopDBContext();
        // GET: adminstrator/Home

        [AuthorizeBusiness]
        public ActionResult Index()
        {
            if(ViewBag.Error != null)
            {
                return View();
            }
            ViewBag.Error = "";
            return View();

        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAccount(string UserName, string Pass,string IPpublic, string UserAgent)
        {
            string codeMd5 = security.HashPass(Pass, UserName);

            //Session["userid"] = 1;
            //Session["userName"] = "Admin";
            //Session["fullName"] = "Le Anh Duc";
            //Session["avatar"] = "~/Data/img/files/ingUser/2.jpg";
            var user = Db.Accounts.SingleOrDefault(x => x.UserName == UserName && x.Password == codeMd5);
            if (user != null)
            {
                if (user.Allowed == 1)
                {
                    Session["userid"] = user.UserId;
                    Session["username"] = user.UserName;
                    Session["fullname"] = user.FullName;
                    Session["avatar"] = user.Avatar;
                    // ghi log phien dang nhap
                    var logUser = new UserLogs();
                    logUser.IDUser = user.UserId;
                    logUser.DateAccess = DateTime.Now;
                    logUser.IPpublic = IPpublic;
                    logUser.UserAgent = UserAgent;
                    Db.UserLogss.Add(logUser);

                    Db.SaveChanges();
                    return RedirectToAction("Index","Accounts");
                }
                else if(user.Allowed == 0)
                {
                    Session["Error"] = "Tài khoản của bạn đã bị khóa vui lòng liên hệ với Admin!";
                    return RedirectToAction("Login");
                }
                
            }
            ViewBag.Error = "Tài khoản hoặc mật khẩu ko chính xác!";
            return RedirectToAction("Login");
            //return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            Session["userid"] = null;
            Session["userName"] = null;
            Session["fullName"] = null;
            Session["avatar"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult NotificationAuthorize()
        {
            return View();
        }


        //foget password
        [HttpPost]
        public ActionResult FogetPassword(string UserName, string AdressMail)
        {
            var User = Db.Accounts.SingleOrDefault(x => x.UserName == UserName && x.Email == AdressMail);
            if (User != null)
            {
                string ramdom_OTP = security.RandomStringGenerator(10);
                Session["ramdom_OTP"] = ramdom_OTP;

                string content = "Ma OTP cua ban la:" + (string)(Session["ramdom_OTP"]);
                bool send_mail_success = security.Send_Email(User.Email, "OTP", content);
                Session["userid"] = User.UserId;
                Session["userName"] = User.UserName;
                Session["userEmail"] = User.Email;
                return RedirectToAction("CheckOTP");
            }
            return RedirectToAction("Login", "Home");
        }

        public ActionResult RenewAccout()
        {
            return View();
        }
        public ActionResult CheckOTP()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OTPauthetication(string OTP)
        {
            if (OTP == (string)(Session["ramdom_OTP"]))
            {
                return RedirectToAction("UpdateAccout");
            }
            else return RedirectToAction("RenewAccout");
        }
        public ActionResult UpdateAccout()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeBusiness]
        public ActionResult Upfile(List<HttpPostedFileBase> files)
        {
            var path = "";
            foreach(var item in files)
            {
                if (item != null)
                {
                    ViewBag.UploafFileError = "";
                    ViewBag.UploafFileSuccess = "";
                    if (item.ContentLength > 0)
                    {
                        var ExtensionFile = Path.GetExtension(item.FileName).ToLower();
                        //if(ExtensionFile==".jpg"|| ExtensionFile == ".png" || ExtensionFile == ".gif" || ExtensionFile == "jpea")
                        //{
                        //    path = Path.Combine(Server.MapPath(@"~/Areas/adminstrator/img"), item.FileName);
                        //    item.SaveAs(path);
                        //    ViewBag.UploafFileSuccess += "UPDATE FILE : " + item.FileName + " success!\n";
                        //}
                        //else if (ExtensionFile == ".mp4" || ExtensionFile == ".mp3")
                        //{
                        //    path = Path.Combine(Server.MapPath(@"~/Areas/adminstrator/img/MUSIC"), item.FileName);
                        //    item.SaveAs(path);
                        //    ViewBag.UploafFileSuccess += "UPDATE FILE : " + item.FileName + " success!\n";
                        //}
                        //else if (ExtensionFile==".xls" || ExtensionFile == ".xlsx")
                        //{
                        //    path = Path.Combine(Server.MapPath(@"~/Areas/adminstrator/img/EXCEL"), item.FileName);
                        //    item.SaveAs(path);
                        //    ViewBag.UploafFileSuccess += "UPDATE FILE : " + item.FileName + " success!\n";
                        //}
                        //else if (ExtensionFile == ".pdf")
                        //{
                        //    path = Path.Combine(Server.MapPath(@"~/Areas/adminstrator/img/PDF"), item.FileName);
                        //    item.SaveAs(path);
                        //    ViewBag.UploafFileSuccess += "UPDATE FILE : " + item.FileName + " success!\n";
                        //}
                        //else if (ExtensionFile == ".doc")
                        //{
                        //    path = Path.Combine(Server.MapPath(@"~/Areas/adminstrator/img/WORD"), item.FileName);
                        //    item.SaveAs(path);
                        //    ViewBag.UploafFileSuccess += "UPDATE FILE : " + item.FileName + " success!\n";
                        //}
                        //else
                        //{
                        //    ViewBag.UploafFileFileError += "UPDATE FILE : " + item.FileName + " error!\n";
                        //}
                        
                            path = Path.Combine(Server.MapPath(@"~/Areas/adminstrator/img"), item.FileName);
                            item.SaveAs(path);
                            ViewBag.UploafFileSuccess += "UPDATE FILE : " + item.FileName + " success!\n";
                        
                    }
                }
            }
            return View();
        }

        public ActionResult Upfile()
        {
            return View();
        }
    }
}