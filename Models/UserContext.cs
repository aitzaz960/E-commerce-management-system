using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample_Web_Application_1.Models
{
    public class UserContext : DbContext 
    {
        public UserContext () :base()
        { }

        public DbSet<User> Users { get; set; }

        public User Get_user(string username, string pwd)
        {
            var Curr_user = from User in Users where User.Username == username && User.Password == pwd select User;
            return Curr_user.FirstOrDefault();
        }

        public User Get_user_by_id(int id_)
        {
            var to_ret = from User in Users where User.UserId == id_ select User;
            return to_ret.FirstOrDefault();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=demo_db;Integrated Security=True;Trusted_Connection=True", builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
            base.OnConfiguring(optionsBuilder);
        }

    }
}
