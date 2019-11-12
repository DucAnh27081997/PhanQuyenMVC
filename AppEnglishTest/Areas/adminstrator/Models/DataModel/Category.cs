using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppEnglishTest.Areas.administrator.Models.DataModel
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int OderNo { get; set; }
        public int Status { get; set; }

        [ForeignKey("Account")]
        public int UserId { get; set; }
        public virtual Account Account { get; set; }
        //public virtual ICollection<Post> Posts { get; set; }

    }
}