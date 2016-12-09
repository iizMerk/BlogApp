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
    [Authorize(roles: "Admin")]
	public class AdminPageViewModel : SignupViewModel 
	{
        //Variables for Change Category and Add Users
        public bool AddUserVisible { get; set; } = false;
        public bool ChangeCategoryVisible { get; set; } = false;
        public string[] CategoryList { get; set; } = {"Post", "AdminPost", "HotPost"};
        public string OldCategory { get; set; }
        public PostCategory NewPostCategory { get; set; }

        public void ShowChangeCategory(int id)
        {
            using (var db = new DatabaseBlog())
            {
                postid = id;
                var post = db.Posts.Find(postid);
                OldCategory = Convert.ToString(post.Category);
                ChangeCategoryVisible = true;
            }
        }
        public void ChangeCategory(int id)
        {
            using (var db = new DatabaseBlog())
            {
                var post = db.Posts.Find(id);
                post.Category = NewPostCategory;
                db.SaveChanges();
                ChangeCategoryVisible = false;
                PostService.LoadPost(Posts);
                
            }
        }
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
            SortExpression = nameof(Post.Date),
            SortDescending = true,
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

