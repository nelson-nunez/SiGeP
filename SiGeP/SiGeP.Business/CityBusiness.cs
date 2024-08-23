using SiGeP.DataAccess.Generic;
using SiGeP.Model.Model.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Business
{
    public class CityBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public CityBusiness(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<City>> GetAsync()
        {
            return await _unitOfWork.AddRepositories.CityRepository.GetAsync();
        }

        public async Task<IEnumerable<City>> GetAllCitiesbyProvince(int provinceId)
        {
            IEnumerable<City> list = new List<City>();
            list = await _unitOfWork.AddRepositories.CityRepository.GetAsync(x => x.ProvinceId == provinceId);
            return list;
        }

        public async Task<City> FindAsync(int id)
        {
            return await _unitOfWork.AddRepositories.CityRepository.FindAsync(id);
        }

        public async Task<int> CitySaveAsync(City entity)
        {
            if (entity.Id == 0)
            {
                await _unitOfWork.AddRepositories.CityRepository.AddAsync(entity);
            }
            else
            {
                _unitOfWork.AddRepositories.CityRepository.Update(entity);
            }

            await _unitOfWork.CompleteAsync();
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entityToDelete = await _unitOfWork.AddRepositories.CityRepository.FindAsync(id);
            _unitOfWork.Delete(entityToDelete);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}

