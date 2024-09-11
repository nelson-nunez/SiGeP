using SiGeP.Business.Base;
using SiGeP.Business.Interfaces;
using SiGeP.DataAccess.Generic;
using SiGeP.Model.Base;
using SiGeP.Model.Model;
using SiGeP.Model.Model.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Business
{
    public class CityBusiness : BusinessBase<City>
    {

        public CityBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<City>> GetAllCitiesbyProvince(int provinceId)
        {
            IEnumerable<City> list = new List<City>();
            list = await unitOfWork.AddRepositories.GetRepository<City>().GetAsync(x => x.ProvinceId == provinceId);
            return list;
        }
    }
}

