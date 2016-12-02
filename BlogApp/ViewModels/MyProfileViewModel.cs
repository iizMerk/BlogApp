using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Controls;
using System.Threading.Tasks;


namespace BlogApp.ViewModels
{
    public class MyProfileViewModel : DotvvmViewModelBase
    {
        //Variables For The ShowPost
        public string TitlePost { get; set; }
        public string TextPost { get; set; }
        //Variables For the MyProfilePage
        public bool IsDisplayed { get; set; } = false;
        public int postid { get; set; }
        public string Message { get; set; } = "My Posts";
        public bool VisibleButton { get; set; } = false;
        public void Show(int id)
        {
            postid = id;
            using (var db = new DatabaseBlog())
            {
                var post = db.Posts.Find(postid);
                TitlePost = post.Title;
                TextPost = post.Text;

            }
            IsDisplayed = true;
        }
        public GridViewDataSet<Post> Posts { get; set; } = new GridViewDataSet<Post>
        {
            SortDescending = false,
            SortExpression = nameof(Post.Date),
            PageSize = 5
        };

        public void CreatePost()
        {
            Context.RedirectToRoute("CreatePost");
        }

        public override Task Load()
        {
            PostService.LoadMyPost(Posts);
            using (var db = new DatabaseBlog())
            {
                if (!db.Posts.Any())
                {
                    Message = "You don't have any post, create one";
                    VisibleButton = true;
                }
            }
            return base.Load();
        }


    }
}

