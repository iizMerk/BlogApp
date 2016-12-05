using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Runtime.Filters;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels
{
    [Authorize]
	public class CreatePostViewModel : DotvvmViewModelBase
	{
        [Required(ErrorMessage ="The Title is Required.")]
        [MaxLength(40,ErrorMessage ="Your title can't contain over 40 charcters.")] 
        public string Title { get; set; }
        [Required(ErrorMessage ="The text is required.")]
        [MinLength(100,ErrorMessage = "Your Post need to have at least 100 characters.")]
        public string Text { get; set; }
        public string[] CategoryList { get; set; } = { "Post", "AdminPost"};
        public PostCategory NewPostCategory { get; set; }

        public void CreatePost()
        {
            var userid = Convert.ToInt32(UserService.GetCurrentUserId());
            using (var db = new DatabaseBlog())
            {
                var user = db.Users.Find(userid);
                var post = new Post();
                post.Title = Title;
                post.Text = Text;
                post.Date = DateTime.Now;
                post.UserID = userid;
                post.Category = NewPostCategory;
                post.CreatorName = user.Username.ToString();
                db.Posts.Add(post);
                db.SaveChanges();
                Context.RedirectToRoute("MyProfile");
            }
        }
    }
}

