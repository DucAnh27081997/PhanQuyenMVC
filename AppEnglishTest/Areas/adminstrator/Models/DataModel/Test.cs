using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppEnglishTest.Areas.administrator.Models.DataModel
{
    // op
    [Table("Test")]
    public class Test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestID { get; set; }

        public string nameTest { get; set; }
        public DateTime dayCreate { get; set; }
        public string linkFileListening { get; set; }
        public bool status { get; set; }

        [ForeignKey("SessionTest")]
        public int SessionTestID { get; set; }

        // thuoc tinh khoa ngoai bang result
        public virtual ICollection<Result> Results { get; set; }

        // thuoc tinh khoa ngoai bang result
        //public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<Past1> Past1 { get; set; }
        public virtual ICollection<Past2> Past2 { get; set; }
        public virtual ICollection<Past3> Past3 { get; set; }
        public virtual ICollection<Past4> Past4 { get; set; }
        public virtual ICollection<Past5> Past5 { get; set; }
        public virtual ICollection<Past6> Past6 { get; set; }
        public virtual ICollection<Past7> Past7 { get; set; }


        public virtual SessionTest SessionTest { get; set; }

        public Test(int SessionTestID, string nameTest,string linkFileListening, bool status = false)
        {
            this.SessionTestID = SessionTestID;
            this.dayCreate = DateTime.Now;
            this.nameTest = nameTest;
        }
    }
}