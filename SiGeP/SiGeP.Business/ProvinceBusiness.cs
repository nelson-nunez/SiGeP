using SiGeP.DataAccess.Generic;
using SiGeP.Model.Model.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Business
{
    public class ProvinceBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public ProvinceBusiness(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Province>> GetAsync()
        {
            return await _unitOfWork.AddRepositories.ProvinceRepository.GetAsync();
        }

        public async Task<Province> FindAsync(int id)
        {
            return await _unitOfWork.AddRepositories.ProvinceRepository.FindAsync(id);
        }

        public async Task<int> ProvinceSaveAsync(Province entity)
        {
            if (entity.Id == 0)
            {
                await _unitOfWork.AddRepositories.ProvinceRepository.AddAsync(entity);
            }
            else
            {
                _unitOfWork.AddRepositories.ProvinceRepository.Update(entity);
            }

            await _unitOfWork.CompleteAsync();
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entityToDelete = await _unitOfWork.AddRepositories.ProvinceRepository.FindAsync(id);
            _unitOfWork.Delete(entityToDelete);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
