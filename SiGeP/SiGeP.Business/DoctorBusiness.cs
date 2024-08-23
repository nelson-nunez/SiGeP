using SiGeP.DataAccess.Generic;
using SiGeP.Model.Base;
using SiGeP.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SiGeP.Business
{
    public class DoctorBusiness
    {
        public readonly UnitOfWork unitOfWork;
        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public DoctorBusiness(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Doctor> FindAsync(int id)
        {
            return await unitOfWork.AddRepositories.DoctorRepository.FindAsync(id);
        }

        public async Task<IEnumerable<Doctor>> GetAsync()
        {
            return await unitOfWork.AddRepositories.DoctorRepository.GetAsync();
        }

        public async Task<IEnumerable<Doctor>> GetListAsync(string specialty)
        {
            IEnumerable<Doctor> list = new List<Doctor>();
            list = await unitOfWork.AddRepositories.DoctorRepository.GetListAsync(x => x.Specialty.ToUpper().Contains(specialty.ToUpper()));
            return list;
        }

        public async Task<PagedDataResponse<Doctor>> GetPagedResultAsync(PagingSortFilterRequest request)
        {
            IEnumerable<Doctor> list = new List<Doctor>();
            Expression<Func<Doctor, bool>> filter = x => true;

            // PARA AGREGAR CLASES CON FILTROS ESPECIALES
            //if (!string.IsNullOrEmpty(cuit))
            //    filter = filter.And(x => x.Person.CUIT.Contains(cuit) || x.Person.PersonDocuments
            //                   .Where(x => x.DocumentTypeId == DocumentType.CuitTypeId)
            //                   .Any(y => y.DocumentNumber.Contains(cuit)));

            var pagedDataResult = await unitOfWork.AddRepositories.DoctorRepository.GetPagedResultAsync(request.FilterBy, request.FilterValue, filter, request.OrderBy, request.PageSize, request.PageIndex);
            return pagedDataResult;
        }

        public async Task<int> DoctorSaveAsync(Doctor entity)
        {
            try
            {
                //Prueba con semáforo
                await _semaphoreSlim.WaitAsync();

                if (entity.Id == 0)
                {
                    await unitOfWork.AddRepositories.DoctorRepository.AddAsync(entity);
                }
                else
                {
                    unitOfWork.AddRepositories.DoctorRepository.Update(entity);
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
            var doctorToDelete = await unitOfWork.AddRepositories.DoctorRepository.FindAsync(id);
            unitOfWork.Delete(doctorToDelete);
            await unitOfWork.CompleteAsync();
            return true;
        }
    }
}
