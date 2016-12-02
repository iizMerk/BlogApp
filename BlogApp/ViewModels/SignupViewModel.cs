using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels
{
    public class SignupViewModel : DotvvmViewModelBase
    {
        [Required(ErrorMessage = "The Username is required")]
        [MinLength(3)]
        public string Username { get; set; }
        [Required(ErrorMessage = "The password is required.")]
        [MinLength(6,ErrorMessage="The password need to have min 6 characters.")]
        public string Password { get; set; }
        [Required(ErrorMessage= "The ConfirmPassword is required.")]
        [Compare("Password", ErrorMessage = "The Password and the ConfirmPassword need to be the same.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "The email is required.")]
        [EmailAddress(ErrorMessage = "This email is not valid.")]
        public string Email { get; set; }
        public string Country { get; set; }

        public string ErrorMessage { get; set; }

        public void SignUp()
        {
            using (var db = new DatabaseBlog())
            {
                var user = new User();
                user.Username = Username;
                user.Password = Password;
                user.Email = Email;
                user.Country = Country;
                if (UserService.CheckEmail(Email) == true)
                {
                    ErrorMessage = "The email is alredy taken";
                    Email = "-";
                }
                else
                {
                    if (UserService.CheckUsername(Username) == true)
                    {
                        ErrorMessage = "The Username is alredy Taken";
                        Username = "-";
                    }
                    else
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                    }
                }


            }
        }
    }
}

