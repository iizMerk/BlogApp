using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Runtime.Filters;

namespace BlogApp.ViewModels
{
    [Authorize]
	public class CreatePostViewModel : DotvvmViewModelBase
	{
        public string Title { get; set; }
        public string Text { get; set; }

        public void CreatePost()
        {

        }
    }
}

