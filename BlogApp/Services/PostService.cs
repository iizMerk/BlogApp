using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.ViewModels
{
    public class PostService
    {
        public static void CreatePost(int userid,string Title , string Text)
        {
            using (var db = new Database())
            {
                var post = new Post();
                post.UserID = userid;
                post.Title = Title;
                post.Text = Text;
                db.Posts.Add(post);
                db.SaveChanges();
            }
        }
    }
}