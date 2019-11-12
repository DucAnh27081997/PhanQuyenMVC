using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AppEnglishTest.Areas.administrator.Models.DataModel
{
    public class ShopDBContext : DbContext
    {
        public ShopDBContext() : base("name=StringConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        //khai báo các thuoc tính map với các table
        public DbSet<Account> Accounts { get; set; }
        public DbSet<UserLogs> UserLogss { get; set; }
        public DbSet<Business> Businesses { get; set; }
        //public DbSet<Category> Categorys { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        //public DbSet<Post> Posts { get; set; }
        public DbSet<GantPermission> GantPermissions { get; set; }

        //public ICollection<UserLoginHistoryAction> UserLoginHistoryActions { get; set; }

        public ICollection<SessionTest> SessionTests { get; set; }
        public ICollection<Test> Tests { get; set; }
        //public ICollection<Question> Questions { get; set; }
        public ICollection<Past1> Past1s { get; set; }

        public ICollection<Past2> Past2s { get; set; }
        public ICollection<Past3> Past3s { get; set; }
        public ICollection<Past4> Past4s { get; set; }
        public ICollection<Past5> Past5s { get; set; }
        public ICollection<Past6> Past6s { get; set; }
        public ICollection<Past7> Past7s { get; set; }
    }
}