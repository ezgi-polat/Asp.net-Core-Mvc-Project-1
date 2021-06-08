using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Entities;

namespace test.Contexts
{
    public class TestContext : IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost; database=Test; integrated security=true");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasMany(I => I.ProductCategories).WithOne(I => I.Product).HasForeignKey(I => I.ProductId);
            modelBuilder.Entity<Category>().HasMany(I => I.ProductCategories).WithOne(I => I.Category).HasForeignKey(I => I.CategoryId);
            modelBuilder.Entity<ProductCategory>().HasIndex(I => new
            {
                I.CategoryId,
                I.ProductId
            }).IsUnique();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
