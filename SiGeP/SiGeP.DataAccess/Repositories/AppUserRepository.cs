using SiGeP.DataAccess.Generic;
using SiGeP.Model;
using SiGeP.Model.ModelUser;

namespace SiGeP.DataAccess.Repositories
{
    public class AppUserRepository : GenericRepository<AppUser>
    {
        public AppUserRepository(DbModelContext context) : base(context)
        { }
    }
}
