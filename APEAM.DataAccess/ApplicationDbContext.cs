using APEAM.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

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

    }
}