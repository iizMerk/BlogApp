namespace BlogApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ViewModels;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogApp.ViewModels.DatabaseBlog>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BlogApp.ViewModels.DatabaseBlog context)
        {
            if (!context.Users.Any())
            {
                var GuestUser = new User
                {
                    Username = "GuestUser",
                    Email = "guestuser@guest.com",
                    Password = "guestguest",
                    Userrole = UserRole.Guest
                };
                var AdminUser = new User
                {
                    Username = "AdminUser",
                    Email = "adminuser@admin.com",
                    Password = "adminadmin",
                    Userrole = UserRole.Admin
                };
                context.Users.Add(GuestUser);
                context.Users.Add(AdminUser);
                context.SaveChanges();
                //  This method will be called after migrating to the latest version.

                //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
                //  to avoid creating duplicate seed data. E.g.
                //
                //    context.People.AddOrUpdate(
                //      p => p.FullName,
                //      new Person { FullName = "Andrew Peters" },
                //      new Person { FullName = "Brice Lambson" },
                //      new Person { FullName = "Rowan Miller" }
                //    );
                //
            }
        }
    }
}
