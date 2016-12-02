using DotVVM.Framework.Controls;
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
            using (var db = new DatabaseBlog())
            {
                var post = new Post();
                post.UserID = userid;
                post.Title = Title;
                post.Text = Text;
                db.Posts.Add(post);
                db.SaveChanges();
            }
        }

        public static void LoadPost(GridViewDataSet<Post> dataset)
        {
            using (var db = new DatabaseBlog())
            {
                var query = from p in db.Posts
                            select p;
                dataset.LoadFromQueryable(query);
            }
        }
        public static void LoadMyPost(GridViewDataSet<Post> dataset)
        {
            var userid = Convert.ToInt32(UserService.GetCurrentUserId());
            using (var db = new DatabaseBlog())
            {
                var query = from p in db.Posts
                            where p.UserID == userid
                            select p;

                dataset.LoadFromQueryable(query);
            }
        }
    }
}