using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppEnglishTest.Areas.administrator.Models.DataModel
{

    [Table("Post")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogId { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public string Picture { get; set; }
        public DateTime CreateDate { get; set; }
        public string Tags { get; set; }

        //[ForeignKey("Category")]
        //public int CategoryId { get; set; }
        public int ViewNo { get; set; }
        public string Status { get; set; }

        [ForeignKey("Account")]
        public int UserId { get; set; }

        //public virtual Category Category { get; set; }

        public virtual Account Account { get; set; }
    }
}