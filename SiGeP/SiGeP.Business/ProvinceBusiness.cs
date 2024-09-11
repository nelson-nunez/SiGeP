using SiGeP.Business.Base;
using SiGeP.DataAccess.Generic;
using SiGeP.Model.Model.Address;

namespace SiGeP.Business
{
    public class ProvinceBusiness: BusinessBase<Province>
    {
        public ProvinceBusiness(UnitOfWork unitOfWork): base(unitOfWork) 
        {
        }

    }
}
