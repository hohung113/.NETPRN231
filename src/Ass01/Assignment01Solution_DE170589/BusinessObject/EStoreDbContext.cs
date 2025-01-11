using BusinessObject.Enitty;
using BusinessObject.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace BusinessObject
{
    public class EStoreDbContext : DbContext
    {
        public EStoreDbContext() { }

        public EStoreDbContext(DbContextOptions<EStoreDbContext> options)
               : base(options)
        {
        }

        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                IConfiguration configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("eStore"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Member
            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.CompanyName)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.Country)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            // Configure Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).ValueGeneratedOnAdd();
                entity.Property(e => e.Freight).HasColumnType("money");
                entity.Property(e => e.OrderDate).HasColumnType("datetime");
                entity.Property(e => e.RequiredDate).HasColumnType("datetime");
                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Member).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Member");
            });

            // Configure OrderDetail
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.ToTable("OrderDetail");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Order");

                entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Product");
            });

            // Configure Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).ValueGeneratedOnAdd();
                entity.Property(e => e.ProductName)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.UnitPrice).HasColumnType("money");
                entity.Property(e => e.Weight)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.HasOne(e => e.Category) 
                   .WithMany(c => c.Products)
                   .HasForeignKey(e => e.CategoryId)
                   .OnDelete(DeleteBehavior.NoAction);
            });

            // Configure Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).ValueGeneratedOnAdd();
                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            });

            // Seed Data
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Beverages" },
                new Category { CategoryId = 2, CategoryName = "Condiments" },
                new Category { CategoryId = 3, CategoryName = "Confections" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Chai", CategoryId = 1, UnitPrice = 18.00m, Weight = "500g" },
                new Product { ProductId = 2, ProductName = "Chang", CategoryId = 1, UnitPrice = 19.00m, Weight = "750g" }
            );

            modelBuilder.Entity<Member>().HasData(
                new Member { MemberId = 1, Email = "john.doe@example.com", Password = "12345", City = "New York", CompanyName = "ABC Corp", Country = "USA" },
                new Member { MemberId = 2, Email = "jane.doe@example.com", Password = "54321", City = "London", CompanyName = "XYZ Ltd", Country = "UK" }
            );
        }
    }
}
