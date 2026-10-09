using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
namespace ECommerceOrderApi.Models
{
    public class AppDbContext:  DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
            public DbSet<Product> Products { get; set; }
        
    }
}
