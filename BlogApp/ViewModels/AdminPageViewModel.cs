using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Controls;
using System.Threading.Tasks;
using DotVVM.Framework.Runtime.Filters;
using BlogApp.ViewModels;

namespace BlogApp.ViewModels
{
    [Authorize/*(roles: "Admin")*/]
	public class AdminPageViewModel : SignupViewModel 
	{
        public bool AddUserVisible { get; set; } = false;

        public void AddUser()
        {
            AddUserVisible = true;
        }
        public void DeletePost(int id)
        {
            using (var db = new DatabaseBlog())
            {
                var post = db.Posts.Find(id);
                db.Posts.Remove(post);
                db.SaveChanges();
                PostService.LoadPost(Posts);
            }
        }
        public void DeleteUser(int id)
        {
            using (var db = new DatabaseBlog())
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                UserService.LoadUser(Users);
            }
        }
        public GridViewDataSet<User> Users { get; set; } = new GridViewDataSet<User>
        {
            SortDescending = false,
            SortExpression = nameof(User.UserID),
            PageSize = 5
        };

        public new GridViewDataSet<Post> Posts { get; set; } = new GridViewDataSet<Post>
        {
            SortDescending = false,
            SortExpression = nameof(Post.Date),
            PageSize = 5
        };

        public override Task Load()
        {
            PostService.LoadPost(Posts);
            UserService.LoadUser(Users);
            return base.Load();
        }
    }
}

