using SiGeP.Business.Base;
using SiGeP.DataAccess.Generic;
using SiGeP.Model.Base;
using SiGeP.Model.Model;
using System.Linq.Expressions;

namespace SiGeP.Business
{
    public class DoctorBusiness: BusinessBase<Doctor>
    {
        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public DoctorBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

 
        public async Task<IEnumerable<Doctor>> GetListAsync(string specialty)
        {
            IEnumerable<Doctor> list = new List<Doctor>();
            list = await unitOfWork.AddRepositories.GetRepository<Doctor>().GetListAsync(x => x.Specialty.ToUpper().Contains(specialty.ToUpper()));
            return list;
        }

        public async Task<int> SaveAsync(Doctor entity)
        {
            try
            {
                //Prueba con semáforo
                await _semaphoreSlim.WaitAsync();

                if (entity.Id == 0)
                {
                    await unitOfWork.AddRepositories.GetRepository<Doctor>().AddAsync(entity);
                }
                else
                {
                    unitOfWork.AddRepositories.GetRepository<Doctor>().Update(entity);
                }
                await unitOfWork.CompleteAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
    }
}
