using APEAM.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace APEAM.DataAccess
{    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ApeamConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<ItemList> ItemLists { get; set; }
    }
}