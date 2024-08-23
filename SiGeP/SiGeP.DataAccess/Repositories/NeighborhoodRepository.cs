using SiGeP.DataAccess.Generic;
using SiGeP.Model.Model;
using SiGeP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiGeP.Model.Model.Address;

namespace SiGeP.DataAccess.Repositories
{
    public class NeighborhoodRepository : GenericRepository<Neighborhood>
    {
        public NeighborhoodRepository(DbModelContext context) : base(context)
        {

        }
    }
}
