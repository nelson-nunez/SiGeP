using SiGeP.DataAccess.Generic;
using SiGeP.Model.Model.Address;
using SiGeP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.DataAccess.Repositories
{
    public class CityRepository : GenericRepository<City>
    {
        public CityRepository(DbModelContext context) : base(context)
        { }
    }
}
