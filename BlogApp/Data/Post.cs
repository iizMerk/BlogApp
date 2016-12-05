using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogApp.ViewModels
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int UserID { get; set; }

        public string CreatorName { get; set; }

        public DateTime Date { get; set; }

        public PostCategory Category { get; set; }
        public virtual ICollection<Comment> Comment { get; set; } = new List<Comment>();
    }
}