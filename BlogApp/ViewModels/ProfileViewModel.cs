using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Controls;

namespace BlogApp.ViewModels
{
    public class ProfileViewModel : DefaultViewModel
    {
        public int userid { get; set; }
        public string CreatorName { get; set; } 

        public GridViewDataSet<Post> profilePosts { get; set; } = new GridViewDataSet<Post>()
        {
            SortExpression = nameof(Post.Date),
            SortDescending = true
        };

        public string GetCreatorName()
        {
            using (var db = new DatabaseBlog())
            {
                var user = db.Users.Find(Convert.ToInt32(Context.Parameters["Id"]));
                return user.Username;
            }
            
        }
        public override Task Init()
        {
            userid = Convert.ToInt32(Context.Parameters["Id"]);
            CreatorName = GetCreatorName();
            PostService.LoadOtherPost(userid, profilePosts);
            return base.Init();        
        }
    }
}

