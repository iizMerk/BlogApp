using DotVVM.Framework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.ViewModels
{
    public class CommentService
    {

        public static void LoadComments(int postid , GridViewDataSet<Comment> dataset)
        {
            using (var db = new DatabaseBlog())
            {
                var query = from p in db.Comments
                            where p.PostID == postid
                            select p;

                dataset.LoadFromQueryable(query);
            }
        }
    }
}