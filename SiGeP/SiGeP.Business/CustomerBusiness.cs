using SiGeP.Business.Base;
using SiGeP.DataAccess.Generic;
using SiGeP.Model.Model;

namespace SiGeP.Business
{
    public class CustomerBusiness: BusinessBase<Customer>
    {
        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public CustomerBusiness(UnitOfWork unitOfWork) : base(unitOfWork) 
        {
        }

        public async Task<IEnumerable<Customer>> GetListAsync(string sellerName)
        {
            IEnumerable<Customer> list = new List<Customer>();
            list = await unitOfWork.AddRepositories.GetRepository<Customer>().GetListAsync(x => x.Person.Name.ToUpper().Contains(sellerName.ToUpper()));
            return list;
        }

        public async Task<int> SaveAsync(Customer entity)
        {
            try
            {
                //Prueba con semáforo
                await _semaphoreSlim.WaitAsync();


                if (entity.Id == 0)
                {
                    await unitOfWork.AddRepositories.GetRepository<Customer>().AddAsync(entity);
                }
                else
                {
                    unitOfWork.AddRepositories.GetRepository<Customer>().Update(entity);
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
