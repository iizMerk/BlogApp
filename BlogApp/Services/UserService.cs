using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace BlogApp.ViewModels
{
    public class UserService
    {

        public static ClaimsIdentity Login(string Usern,string Password)
        {
            using (var db = new Database())
            {
                var query = from p in db.Users
                            where p.Username == Usern && p.Password == Password
                            select p;
                var user = query.SingleOrDefault();
                if (query.Count() != 0)
                {
                    var claim = new List<Claim>();
                    claim.Add(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.UserID)));
                    claim.Add(new Claim(ClaimTypes.Name, user.Username));
                    claim.Add(new Claim(ClaimTypes.Role, Convert.ToString(user.Userrole)));

                    var identity = new ClaimsIdentity(claim);
                    return identity;
                }
            }
            return null;
        }

        public static int? GetCurrentUserId()
        {
            var identity = HttpContext.Current.GetOwinContext().Authentication.User.Identity as ClaimsIdentity;
            if (identity.IsAuthenticated)
            {
                var id = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Convert.ToInt32(id);

            }
            else
            {
                return null;
            }
        }
    }
}