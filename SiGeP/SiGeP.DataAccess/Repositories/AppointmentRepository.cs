using SiGeP.DataAccess.Generic;
using SiGeP.Model.Model.Address;
using SiGeP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiGeP.Model.Model;

namespace SiGeP.DataAccess.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>
    {
        public AppointmentRepository(DbModelContext context) : base(context)
        { }
    }
}
