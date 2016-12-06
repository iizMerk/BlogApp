using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using DotVVM.Framework.Controls;

namespace BlogApp.ViewModels
{
    public class DefaultViewModel : DotvvmViewModelBase
    {
        //Variables For The Users.
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public bool IsVisible { get; set; } = false;
        //For Load More Posts
        public bool LoadMoreVisible { get; set; }
        public string LoadMoreMessage { get; set; }

        //For Load More ImportantPosts
        public string ImportantPostMessage { get; set; }
        public bool ImportantPostVisible { get; set; }

        public int PostCount { get; set; }

        //Variable For the Post

        //Variables For The Comments

        public bool CommentVisible { get; set; } = false;
        public int pid { get; set; }
        public string CommentText { get; set; }
        public string CommentError { get; set; }
        public string CommentErrorColor { get; set; }
        public bool DeleteCommentVisible { get; set; } = false;

        //Variables For The Site 
        public string BlogAppTitle { get; set; } = "Welcome to BlogApp";

        public string ErrorMessage { get; set; }

        //variables for pass the userid on the profilepage
        public int useridprofile { get; set; }

        //Method for the comments
        public void DeleteComment(int postid, int CommentID)
        {
            using (var db = new DatabaseBlog())
            {
                var comment = db.Comments.Find(CommentID);
                var post = db.Posts.Find(postid);
                db.Comments.Remove(comment);
                db.SaveChanges();
                post.CommentCount = post.CommentCount = CommentService.CommentCount(postid);
                db.SaveChanges();
                CommentService.LoadComments(postid, Comments);
                PostService.LoadPost(Posts);
            }
        }

        public int PassTheID(int userid)
        {
            return userid;
        }
        public void ShowComment(int postid)
        {
            pid = postid;
            CommentService.LoadComments(postid, Comments);
            CommentVisible = true;
        }
        public void CreateComment(int postid)
        {
            using (var db = new DatabaseBlog())
            {
                var user = db.Users.Find(Convert.ToInt32(UserService.GetCurrentUserId()));
                var post = db.Posts.Find(postid);
                var comm = new Comment();
                comm.comment = CommentText ?? "no";
                comm.UserID = user.UserID;
                comm.Username = user.Username;
                comm.Date = DateTime.Now;
                if (comm.comment == "no")
                {
                    CommentErrorColor = "red";
                    CommentError = "You can't publish an empty comment.";
                }
                else
                {
                    post.Comment.Add(comm);
                    db.SaveChanges();
                    post.CommentCount = CommentService.CommentCount(postid);
                    db.SaveChanges();
                    CommentText = null;
                    CommentErrorColor = "green";
                    CommentError = "Your comment as been published successfully.";
                    CommentService.LoadComments(postid, Comments);
                    PostService.LoadPost(Posts);
                }

            }
        }
        //Method For Posts and Login/SignOut
        public void NewPost()
        {
            using (var db = new DatabaseBlog())
            {

            }
        }

        public void Login()
        {
            using (var db = new DatabaseBlog())
            {
                var identity = UserService.Login(Username, Password);
                if (identity != null)
                {
                    Context.OwinContext.Authentication.SignIn(identity);
                    Context.RedirectToRoute("Default");
                }
                else
                {
                    ErrorMessage = "Your Email or Password Are incorrect.";
                }
            }
        }


        public void LoadMore()
        {
            Posts.PageSize = Posts.PageSize + 4;
            PostService.LoadPost(Posts);
            CheckLoadMore();
        }

        public void LoadMoreImportantPost()
        {
            ImportantPosts.PageSize = ImportantPosts.PageSize + 1;
            PostService.LoadImportantPost(ImportantPosts);
            CheckImportantPost();
        }
        public void SignOut()
        {
            Context.OwinContext.Authentication.SignOut();
            Context.RedirectToRoute("Default");
        }
        public void SignUp()
        {
            Context.RedirectToRoute("SignUp");
        }

        public void CreateNewPost()
        {
            if (UserService.GetCurrentUserId() == null)
            {
                IsVisible = true;
            }
            else
            {
                Context.RedirectToRoute("CreatePost");
            }
        }

        public void Show()
        {
            IsVisible = true;
        }

        public void Register()
        {
            using (var db = new DatabaseBlog())
            {

            }
        }

        public GridViewDataSet<Post> Posts { get; set; } = new GridViewDataSet<Post>()
        {
            SortExpression = nameof(Post.Date),
            SortDescending = true,
            PageSize = 4

        };

        public GridViewDataSet<Post> ImportantPosts { get; set; } = new GridViewDataSet<Post>()
        {
            SortExpression = nameof(Post.Date),
            SortDescending = true,
            PageSize = 1
        };

        public GridViewDataSet<Comment> Comments { get; set; } = new GridViewDataSet<Comment>()
        {
            SortExpression = nameof(Comment.Date),
            SortDescending = false
        };

        public string GetCreatorName(int userid)
        {
            using (var db = new DatabaseBlog())
            {
                var user = db.Users.Find(userid);
                return user.Username.ToString();
            }
        }

        public void CheckLoadMore()
        {
            if (Posts.TotalItemsCount <= Posts.PageSize)
            {
                LoadMoreVisible = false;
                LoadMoreMessage = "No other post to load.";
            }
            else
            {
                LoadMoreMessage = "";
                LoadMoreVisible = true;
            }
        }

        public void CheckImportantPost()
        {

            if (ImportantPosts.TotalItemsCount <= ImportantPosts.PageSize)
            {
                ImportantPostVisible = false;
                ImportantPostMessage = "No other important post to load.";
            }
            else
            {
                LoadMoreMessage = "";
                ImportantPostVisible = true;
            }

        }
        public override Task Load()
        {
            PostService.LoadImportantPost(ImportantPosts);
            PostService.LoadPost(Posts);
            CheckImportantPost();
            CheckLoadMore();
            PostCount = Posts.TotalItemsCount;
            ImportantPosts.PageSize = 1;
            Posts.PageSize = 4;
            return base.Load();
        }
    }
}
