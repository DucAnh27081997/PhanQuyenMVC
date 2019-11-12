using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AppEnglishTest.Areas.adminstrator.Models.DataModel;

namespace AppEnglishTest.Areas.administrator.Models.DataModel
{
    [Table("Account")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string IPpublic { get; set; }
        public string Avatar { get; set; }
        public byte? isAdmin { get; set; }
        public byte? Allowed { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Checked { get; set; }

        // thuoc tinh Natigation
        // thuoc tinh khoa ngoai bang GantPermission
        public ICollection<GantPermission> GantPermissions { get; set; }
        public ICollection<UserLogs> UserLogss { get; set; }

        // thuoc tinh khoa ngoai bang result
        public ICollection<Result> Results { get; set; }
        //public ICollection<Post> Posts { get; set; }
        //public ICollection<Category> Categorys { get; set; }
    }
}