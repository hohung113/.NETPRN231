using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyStoreDB"));
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Beverages" },
                new Category { CategoryId = 2, CategoryName = "Condiments" },
                new Category { CategoryId = 3, CategoryName = "Confections" },
                new Category { CategoryId = 4, CategoryName = "Dairy Products" },
                new Category { CategoryId = 5, CategoryName = "Grains/Cereals" },
                new Category { CategoryId = 6, CategoryName = "Meat/Poultry" },
                new Category { CategoryId = 7, CategoryName = "Produce" },
                new Category { CategoryId = 8, CategoryName = "Seafood" }
            );

            modelBuilder.Entity<Product>().HasData(
           new Product
           {
               ProductId = 1,
               ProductName = "Chai",
               CategoryId = 1,
               UnitsInStock = 39,
               UnitPrice = 18.00m
           },
           new Product
           {
               ProductId = 2,
               ProductName = "Chang",
               CategoryId = 1,
               UnitsInStock = 17,
               UnitPrice = 19.00m
           },
           new Product
           {
               ProductId = 3,
               ProductName = "Aniseed Syrup",
               CategoryId = 2,
               UnitsInStock = 13,
               UnitPrice = 10.00m
           },
           new Product
           {
               ProductId = 4,
               ProductName = "Chef Anton's Cajun Seasoning",
               CategoryId = 2,
               UnitsInStock = 53,
               UnitPrice = 22.00m
           },
           new Product
           {
               ProductId = 5,
               ProductName = "Chef Anton's Gumbo Mix",
               CategoryId = 2,
               UnitsInStock = 0,
               UnitPrice = 21.35m
           });
        }
    }
}
