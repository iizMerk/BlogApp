using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace BlogApp.ViewModels
{
    public class DefaultViewModel : DotvvmViewModelBase
    {

        public string Title { get; set; } = "Welcome to BlogApp";


        

    }
}
