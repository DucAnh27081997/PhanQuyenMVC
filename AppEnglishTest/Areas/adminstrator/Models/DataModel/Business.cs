using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppEnglishTest.Areas.administrator.Models.DataModel
{
    // lớp này lưu trữ các tên controller
    [Table("Business")]
    public class Business
    {
        [Key]
        public string BusinessId { get; set; }

        public string BusinessName { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}