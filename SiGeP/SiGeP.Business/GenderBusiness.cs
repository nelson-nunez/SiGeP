using SiGeP.DataAccess.Generic;
using SiGeP.Model.Model;

namespace SiGeP.Business
{
    public class GenderBusiness
    {
        public readonly UnitOfWork unitOfWork;
        public GenderBusiness(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Gender>> GetAsync()
        {
            return await unitOfWork.Repositories.GenderRepository.GetAsync();
        }

        public async Task<IEnumerable<Gender>> GetListAsync(string name)
        {
            IEnumerable<Gender> list = new List<Gender>();
            list = await unitOfWork.Repositories.GenderRepository.GetListAsync(x => x.Name.ToUpper().Contains(name.ToUpper()));
            return list;
        }
    }
}
