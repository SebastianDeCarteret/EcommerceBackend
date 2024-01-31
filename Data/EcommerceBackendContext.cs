using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Data
{
    public class EcommerceBackendContext : DbContext
    {
        public EcommerceBackendContext(DbContextOptions<EcommerceBackendContext> options)
            : base(options)
        {
        }

        public DbSet<EcommerceBackend.Models.User> User { get; set; } = default!;
        public DbSet<EcommerceBackend.Models.Product> Product { get; set; } = default!;
        public DbSet<EcommerceBackend.Models.Category> Category { get; set; } = default!;
        public DbSet<EcommerceBackend.Models.Basket> Basket { get; set; } = default!;
        public DbSet<EcommerceBackend.Models.Review> Review { get; set; } = default!;
        public DbSet<EcommerceBackend.Models.Order> Order { get; set; } = default!;
        public DbSet<EcommerceBackend.Models.Session> Session { get; set; } = default!;
    }
}
