
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext: IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories  { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options): base(options) 
        {
            
        }

    }
}
