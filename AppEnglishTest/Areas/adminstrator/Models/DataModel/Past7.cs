using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppEnglishTest.Areas.administrator.Models.DataModel
{
    [Table("Past7")]
    public class Past7
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Past7_ID { get; set; }
        public int index { get; set; }
        public string detail_question { get; set; }
        public string A { get; set;}
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string answer { get; set; }
        public string detail { get; set; }
        public int Part { get; set; } // cac part trong bai thi TOEIC


        // thuoc tinh khoa ngoai test
        [ForeignKey("Test")]
        public int TestID { get; set; }
        public virtual Test Test { get; set; }
    }
}