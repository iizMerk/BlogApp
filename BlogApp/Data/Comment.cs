using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogApp.ViewModels
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        public string comment { get; set; }
        public string Username { get; set; }

        public bool Delete { get; set; } = false;
        public DateTime Date { get; set; }
        public int UserID { get; set; }
        public int PostID { get; set; }
    }
}