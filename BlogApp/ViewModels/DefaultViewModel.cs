using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BlogApp.ViewModels
{
    public class DefaultViewModel : DotvvmViewModelBase
    {
        //Variables For The Users.
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }

        //Variable For the Post

        //Variables For The Comments

        //Variables For The Site 
        public string BlogAppTitle { get; set; } = "Welcome to BlogApp";

        public void NewPost()
        {
            using (var db = new Database())
            {
                
            }
        }

        public void Login()
        {
            using (var db = new Database())
            {
 
                
            }
        }

        public void Register()
        {
            using (var db= new Database())
            {

            }
        }
    }
}
