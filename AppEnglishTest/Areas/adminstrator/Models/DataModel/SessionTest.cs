using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppEnglishTest.Areas.administrator.Models.DataModel
{
    [Table("SessionTest")]
    public class SessionTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionTestID { get; set;}

        public DateTime dayStart { get; set; }

        public DateTime dayEnd { get; set; }

        public string Name { get; set;}

        public Boolean status { get; set;}

        public virtual ICollection<Test> Test { get; set; }

    }
}