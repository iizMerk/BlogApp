using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogApp.ViewModels
{
    public class Database:DbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Post> Posts { get; set; }

        DbSet<Comment> Comments { get; set; }

        public Database() : base("name=Connection") { }

    }
}