using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample_Web_Application_1.Models
{
    public class AdminContext : DbContext
    {
        public AdminContext () : base()
        { }

        public DbSet<Admin> Admins { get; set; }

        public Admin Get_admin(string username, string pwd, string my_key)
        {
            var Curr_admin = from Admin in Admins where Admin.Username == username && Admin.Password == pwd && Admin.Account_key == my_key select Admin;
            return Curr_admin.FirstOrDefault();
        }

        public Admin Get_admin_by_id(int id_)
        {
            var to_ret = from Admin in Admins where Admin.AdminId == id_ select Admin;
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
