using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppEnglishTest.Areas.adminstrator.Models.DataModel
{
    public class PermissionAction
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public bool isGranted { get; set; }
        public string Description { get; set; }


    }
}