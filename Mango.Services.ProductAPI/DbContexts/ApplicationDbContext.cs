using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Test",
                Price = 10.99,
                Description = "Test",
                ImageUrl = "https://netms.blob.core.windows.net/mango/anna-tukhfatullina-food-photographer-stylist-Mzy-OjtCI70-unsplash.jpg",
                CategoryName = "Dessert",
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "tikki takka",
                Price = 13.99,
                Description = "Test",
                ImageUrl = "https://netms.blob.core.windows.net/mango/anh-nguyen-kcA-c3f_3FE-unsplash.jpg",
                CategoryName = "Appetizer",
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Rajin bari",
                Price = 15,
                Description = "Test",
                ImageUrl = "https://netms.blob.core.windows.net/mango/eiliv-aceron-ZuIDLSz3XLg-unsplash.jpg",
                CategoryName = "Entree",
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Delicioza",
                Price = 12.2,
                Description = "Test test",
                ImageUrl = "https://netms.blob.core.windows.net/mango/anna-pelzer-IGfIGP5ONV0-unsplash.jpg",
                CategoryName = "Yummy",
            });
        }
    }
}
