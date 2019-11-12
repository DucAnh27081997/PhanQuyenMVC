using System.Data.Entity;
using AppEnglishTest.Areas.administrator.Models.DataModel;
using AppEnglishTest.Areas.administrator.Helper;

namespace AppEnglishTest.Areas.administrator.Models.BusinessModel
{
    public class ShopDataBaseInitializer : DropCreateDatabaseIfModelChanges<ShopDBContext>
    {
        protected override void Seed(ShopDBContext context)
        {
            var Admin = new Account
            {
                UserName = "Admin",
                Password = security.HashPass("adminadmin", "Admin"),
                Avatar = "",
                IPpublic = "192.168.1.0",
                Allowed = 1,
                isAdmin = 1,
                Email = "anhduc2781997@gmail.com",
                FullName = "Le Anh Duc",
                PhoneNumber = "15150076"
            };
            context.Accounts.Add(Admin);

            for (int i = 0; i < 10; i++)
            {
                var User = new Account();
                
                User.UserName = "User" + i.ToString();
                User.Password = security.HashPass("adminadmin", User.UserName);
                User.Avatar = "";
                User.IPpublic = "192.168.1.0";
                User.Allowed = 1;
                User.isAdmin = 0;
                User.Email = "anhduc2781997@gmail.com";
                User.FullName = "Le Anh Duc";
                User.PhoneNumber = "15150076";

                context.Accounts.Add(User);
            }

            context.SaveChanges();
        }
    }
}