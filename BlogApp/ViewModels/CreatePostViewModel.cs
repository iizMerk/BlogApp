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
        public string Title { get; set; }
        [Required(ErrorMessage ="The text is required.")]
        [MinLength(100,ErrorMessage = "Your Post need to have at least 100 characters.")]
        public string Text { get; set; }

        public void CreatePost()
        {
            var userid = Convert.ToInt32(UserService.GetCurrentUserId());
            using (var db = new DatabaseBlog())
            {
                var post = new Post();
                post.Title = Title;
                post.Text = Text;
                post.Date = DateTime.Now;
                post.UserID = userid;
                db.Posts.Add(post);
                db.SaveChanges();
                Context.RedirectToRoute("MyProfile");
            }
        }
    }
}

