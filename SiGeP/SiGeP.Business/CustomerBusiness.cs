using SiGeP.DataAccess.Generic;
using SiGeP.Model.Base;
using SiGeP.Model.Model;
using System.Linq.Expressions;

namespace SiGeP.Business
{
    public class CustomerBusiness
    {
        public readonly UnitOfWork unitOfWork;
        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);


        public CustomerBusiness(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Customer> FindAsync(int id)
        {
            return await unitOfWork.AddRepositories.CustomerRepository.FindAsync(id);
        }


        public async Task<IEnumerable<Customer>> GetAsync()
        {
            return await unitOfWork.AddRepositories.CustomerRepository.GetAsync();
        }

        public async Task<IEnumerable<Customer>> GetListAsync(string sellerName)
        {
            IEnumerable<Customer> list = new List<Customer>();
            list = await unitOfWork.AddRepositories.CustomerRepository.GetListAsync(x => x.Person.Name.ToUpper().Contains(sellerName.ToUpper()));
            return list;
        }

        public async Task<PagedDataResponse<Customer>> GetPagedResultAsync(PagingSortFilterRequest request)
        {
            IEnumerable<Customer> list = new List<Customer>();
            Expression<Func<Customer, bool>> filter = x => true;

            // PARA AGREGAR CLASES CON FILTROS ESPECIALES
            //if (!string.IsNullOrEmpty(cuit))
            //    filter = filter.And(x => x.Person.CUIT.Contains(cuit) || x.Person.PersonDocuments
            //                   .Where(x => x.DocumentTypeId == DocumentType.CuitTypeId)
            //                   .Any(y => y.DocumentNumber.Contains(cuit)));

            var pagedDataResult = await unitOfWork.AddRepositories.CustomerRepository.GetPagedResultAsync(request.FilterBy, request.FilterValue, filter, request.OrderBy, request.PageSize, request.PageIndex);
            return pagedDataResult;
        }

        public async Task<int> CustomerSaveAsync(Customer entity)
        {
            try
            {
                //Prueba con semáforo
                await _semaphoreSlim.WaitAsync();


                if (entity.Id == 0)
                {
                    await unitOfWork.AddRepositories.CustomerRepository.AddAsync(entity);
                }
                else
                {
                    unitOfWork.AddRepositories.CustomerRepository.Update(entity);
                }
                await unitOfWork.CompleteAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customerToDelete = await unitOfWork.AddRepositories.CustomerRepository.FindAsync(id);
            unitOfWork.Delete(customerToDelete);
            await unitOfWork.CompleteAsync();
            return true;
        }
    }
}
