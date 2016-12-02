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
        public bool IsDisplayed { get; set; } = false;
        public int postid { get; set; }
        public string Message { get; set; } = "My Posts";
        public bool VisibleButton { get; set; } = false;
        public void Show(int id)
        {
            IsDisplayed = true;
            postid = id;
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
            if (Posts == null)
            {
                Message = "No Post";
                VisibleButton = true;
            }
            return base.Load();
        }


    }
}

