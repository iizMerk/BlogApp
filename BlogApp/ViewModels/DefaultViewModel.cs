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

        public string ErrorMessage { get; set; }

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

        public void Register()
        {
            using (var db = new Database())
            {

            }
        }
    }
}
