using SiGeP.DataAccess.Generic;
using SiGeP.Model.Base;
using SiGeP.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Business
{
    public class AppointmentBusiness
    {
        private readonly UnitOfWork unitOfWork;
        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public AppointmentBusiness(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Appointment> FindAsync(int id)
        {
            return await unitOfWork.AddRepositories.AppointmentRepository.FindAsync(id);
        }

        public async Task<IEnumerable<Appointment>> GetAsync()
        {
            return await unitOfWork.AddRepositories.AppointmentRepository.GetAsync();
        }

        //public async Task<IEnumerable<Appointment>> GetListAsync(DateTime date)
        //{
        //    IEnumerable<Appointment> list = new List<Appointment>();
        //    list = await unitOfWork.AddRepositories.AppointmentRepository.GetListAsync(x => x.Date.Date == date.Date);
        //    return list;
        //}

        public async Task<PagedDataResponse<Appointment>> GetPagedResultAsync(PagingSortFilterRequest request)
        {
            IEnumerable<Appointment> list = new List<Appointment>();
            Expression<Func<Appointment, bool>> filter = x => true;

            // Añadir filtros específicos aquí si es necesario
            // Por ejemplo, si quieres filtrar por médico o cliente:
            // if (!string.IsNullOrEmpty(request.DoctorName))
            //     filter = filter.And(x => x.Doctor.Name.ToUpper().Contains(request.DoctorName.ToUpper()));

            var pagedDataResult = await unitOfWork.AddRepositories.AppointmentRepository.GetPagedResultAsync(
                request.FilterBy, request.FilterValue, filter, request.OrderBy, request.PageSize, request.PageIndex);

            return pagedDataResult;
        }

        public async Task<int> AppointmentSaveAsync(Appointment entity)
        {
            try
            {
                // Prueba con semáforo
                await _semaphoreSlim.WaitAsync();

                if (entity.Id == 0)
                {
                    await unitOfWork.AddRepositories.AppointmentRepository.AddAsync(entity);
                }
                else
                {
                    unitOfWork.AddRepositories.AppointmentRepository.Update(entity);
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
            var appointmentToDelete = await unitOfWork.AddRepositories.AppointmentRepository.FindAsync(id);
            unitOfWork.Delete(appointmentToDelete);
            await unitOfWork.CompleteAsync();
            return true;
        }
    }
}
