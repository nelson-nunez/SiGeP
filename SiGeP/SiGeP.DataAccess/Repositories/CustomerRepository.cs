using SiGeP.DataAccess.Generic;
using SiGeP.Model;
using SiGeP.Model.Model;

namespace SiGeP.DataAccess.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(DbModelContext context) : base(context)
        {

        }
    }
}