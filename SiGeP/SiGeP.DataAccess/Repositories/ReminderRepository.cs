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
    public class ReminderRepository : GenericRepository<Reminder>
    {
        public ReminderRepository(DbModelContext context) : base(context)
        {

        }
    }
}
