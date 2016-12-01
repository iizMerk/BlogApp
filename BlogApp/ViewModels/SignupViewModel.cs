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
        [Required(ErrorMessage ="The Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "The password is required.")]
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage = "The Password and the ConfirmPassword need to be the same.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="The email is required.")]
        [EmailAddress(ErrorMessage = "This email is not valid.")]
        public string  Email { get; set; }
        public string Country { get; set; }

        public string  ErrorMessage { get; set; }

        public void SignUp()
        {
            using (var db = new DatabaseBlog())
            {
                var user = new User();
                user.Username = Username;
                user.Password = Password;
                user.Email = Email;
                user.Country = Country;
                UserService.CheckEmail(Email);
                UserService.CheckUsername(Username);
                if (true)
                {

                }
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
    }
}

