using ecommerceTask.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerceTask.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SocialProfile> socialProfiles  { get; set; }
        public DbSet<Cart> Carts  { get; set; }
        public DbSet<CartItem> CartItems  { get; set; }
        public DbSet<Order> Orders  { get; set; }
        public DbSet<OrderLine> orderLines { get; set; }
        public DbSet<Credentials> Credentials { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> Categories { get; set; }


    }
}
