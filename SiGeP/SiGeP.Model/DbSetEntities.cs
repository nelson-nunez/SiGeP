using Microsoft.EntityFrameworkCore;
using SiGeP.Model.Model;
using SiGeP.Model.ModelUser;

namespace SiGeP.Model
{
    public class DbSetEntities
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
