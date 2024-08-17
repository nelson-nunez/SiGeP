using SiGeP.DataAccess.Generic;
using SiGeP.Model;
using SiGeP.Model.Model;

namespace SiGeP.DataAccess.Repositories
{
    public class GenderRepository : GenericRepository<Gender>
    {
        public GenderRepository(DbModelContext context) : base(context)
        {

        }
    }
}


