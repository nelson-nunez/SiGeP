using SiGeP.DataAccess.Generic;
using SiGeP.Model.Model.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Business
{
    public class NeighborhoodBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public NeighborhoodBusiness(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Neighborhood>> GetAsync()
        {
            return await _unitOfWork.AddRepositories.NeighborhoodRepository.GetAsync();
        }

        public async Task<IEnumerable<Neighborhood>> GetAllNeighborhoodsbyCity(int cityId)
        {
            IEnumerable<Neighborhood> list = new List<Neighborhood>();
            list = await _unitOfWork.AddRepositories.NeighborhoodRepository.GetAsync(x => x.CityId == cityId);
            return list;
        }

        public async Task<Neighborhood> FindAsync(int id)
        {
            return await _unitOfWork.AddRepositories.NeighborhoodRepository.FindAsync(id);
        }

        public async Task<int> NeighborhoodSaveAsync(Neighborhood entity)
        {
            if (entity.Id == 0)
            {
                await _unitOfWork.AddRepositories.NeighborhoodRepository.AddAsync(entity);
            }
            else
            {
                _unitOfWork.AddRepositories.NeighborhoodRepository.Update(entity);
            }

            await _unitOfWork.CompleteAsync();
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entityToDelete = await _unitOfWork.AddRepositories.NeighborhoodRepository.FindAsync(id);
            _unitOfWork.Delete(entityToDelete);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
