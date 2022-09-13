using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQefNWLibrary.Models {

    public class AppDbContext : DbContext {

        public DbSet<Employee> Employees { get; set; } = null!; //! says don't worry about this issue compilier.
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) { //needed for non-web app
            string connStr = @"server=localhost\sqlexpress;" +
                            "database=Northwind;" +
                            "trusted_connection=true;";
            if (!builder.IsConfigured) {
                builder.UseSqlServer(connStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<OrderDetail>(o => {
                o.ToTable("Order Details");
                o.HasKey(x => new { x.OrderId, x.ProductId }); //these lines build/connect the order details table that has a space in it.
            });
        }
        
    }
}
