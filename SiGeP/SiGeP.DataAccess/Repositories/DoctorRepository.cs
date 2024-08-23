using SiGeP.DataAccess.Generic;
using SiGeP.Model;
using SiGeP.Model.Model;

namespace SiGeP.DataAccess.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>
    {
        public DoctorRepository(DbModelContext context) : base(context)
        {

        }
    }
}
