using SiGeP.Business.Base;
using SiGeP.DataAccess.Generic;
using SiGeP.Model.Model.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Business
{
    public class NeighborhoodBusiness: BusinessBase<Neighborhood>
    {
        public NeighborhoodBusiness(UnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }

        public async Task<IEnumerable<Neighborhood>> GetAllNeighborhoodsbyCity(int cityId)
        {
            IEnumerable<Neighborhood> list = new List<Neighborhood>();
            list = await unitOfWork.AddRepositories.GetRepository<Neighborhood>().GetAsync(x => x.CityId == cityId);
            return list;
        }
    }
}
