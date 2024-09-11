using SiGeP.Business.Base;
using SiGeP.DataAccess.Generic;
using SiGeP.Model.Model;

namespace SiGeP.Business
{
    public class GenderBusiness: BusinessBase<Gender>
    {
        public GenderBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<Gender>> GetListAsync(string name)
        {
            IEnumerable<Gender> list = new List<Gender>();
            list = await unitOfWork.AddRepositories.GetRepository<Gender>().GetListAsync(x => x.Name.ToUpper().Contains(name.ToUpper()));
            return list;
        }
    }
}
