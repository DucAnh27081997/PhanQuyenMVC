using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppEnglishTest.Areas.administrator.Models.DataModel
{
    [Table("Result")]
    public class Result
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime practiceDay { get; set; }     
        public int correctSentences { get; set; }
        public int incorrectSentences { get; set; }
        public int scores { get; set;}

        // thuoc tinh khoa ngoai bang Test
        [ForeignKey("Test")]
        [Column(Order = 1)]
        public int TestID { get; set; }
        public virtual Test Test { get; set; }

        // thuoc tinh khoa ngoai bang User
        [ForeignKey("Account")]
        [Column(Order = 2)]
        public int UserId { get; set; }
        public virtual Account Account { get; set; }
    }
}