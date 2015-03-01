using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.EFContext
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }

    public class User
    {
        [Key]
        public string Login { get; set; }
        public string Password { get; set; }
    }
}