using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppEnglishTest.Areas.administrator.Models.DataModel
{
    [Table("UserLogs")]
    public class UserLogs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserLogsID { get; set; }

        [ForeignKey("Account")]
        public int IDUser { get; set; }
        public string IPpublic { get; set; }
        public string UserAgent { get; set;}
        public DateTime DateAccess { get; set;}
      
        public virtual Account Account { get; set; }

        //public ICollection<UserLoginHistoryAction> UserLoginHistoryActions { get; set; }
    }
}