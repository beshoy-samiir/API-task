using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APITask.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Mobile" });
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 2, Name = "Laptop" });
            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 1, Name = "IPhone", Price = 20000, CatId = 1 });
            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 2, Name = "Samsung", Price = 15000, CatId = 1 });
            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 3, Name = "Oppo", Price = 9000, CatId = 1 });
            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 4, Name = "Dell", Price = 25000, CatId = 2 });
            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 5, Name = "Hp", Price = 17000, CatId = 2 });

            base.OnModelCreating(modelBuilder);
        }
    }
}
