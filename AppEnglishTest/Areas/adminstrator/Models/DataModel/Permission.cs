using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppEnglishTest.Areas.administrator.Models.DataModel
{
    [Table("Permission")]
    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }


        [ForeignKey("Business")]
        public string BusinessId { get; set; }

        // thuoc tinh navigation 
        public virtual Business Business { get; set; }

        public virtual ICollection<GantPermission> GantPermissions { get; set; }

    }
}