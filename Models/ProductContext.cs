using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample_Web_Application_1.Models
{
    public class ProductContext : DbContext 
    {
        public ProductContext() : base()
        { }

        public DbSet<Product> Products { get; set; }

        public Product Get_product_by_id(int id_)
        {
            var to_ret = from Product in Products where Product.ProductId == id_ select Product;
            //Console.WriteLine("We received the request to return the product with product id: " + id_);
            return to_ret.FirstOrDefault();
        }

        public IQueryable<Product> get_available_products()
        {
            var to_ret = from Product in Products where Product.status == "Available" select Product;
            return to_ret;
        }

        public IQueryable<Product> get_pending_products()
        {
            var to_ret = from Product in Products where Product.status == "Pending" select Product;
            return to_ret;
        }

        public IQueryable<Product> get_purchase_history_of_user(int user_id)
        {
            var to_ret = from Product in Products where Product.status == "Sold" && Product.BuyerId == user_id select Product;
            return to_ret;
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
