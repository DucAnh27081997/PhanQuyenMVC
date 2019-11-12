using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppEnglishTest.Areas.administrator.Models.DataModel
{
    [Table("GrantPermission")]
    public class GantPermission
    {
        [Key]
        [Column(Order = 1) ]
        [ForeignKey("Permission")]
  
        public int PermissionId { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Account")]
        public int UserId { get; set; }
        public string Description { get; set; }

        // thuoc tinh navigation 

        public virtual Account Account { get; set; }

        public virtual Permission Permission { get; set; }

    }
}