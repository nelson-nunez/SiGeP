using SiGeP.Business.Notifiers;
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
    aca quede, agregar interfaz para los bussines y completar el guardado de los reminders
    public class ReminderBusiness
    {
        private readonly UnitOfWork unitOfWork;
        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public ReminderBusiness(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Reminder> FindAsync(int id)
        {
            return await unitOfWork.AddRepositories.ReminderRepository.FindAsync(id);
        }

        public async Task<IEnumerable<Reminder>> GetAsync()
        {
            return await unitOfWork.AddRepositories.ReminderRepository.GetAsync();
        }

        public async Task<PagedDataResponse<Reminder>> GetPagedResultAsync(PagingSortFilterRequest request)
        {
            IEnumerable<Reminder> list = new List<Reminder>();
            Expression<Func<Reminder, bool>> filter = x => true;

            // Añadir filtros específicos aquí si es necesario

            var pagedDataResult = await unitOfWork.AddRepositories.ReminderRepository.GetPagedResultAsync(
                request.FilterBy, request.FilterValue, filter, request.OrderBy, request.PageSize, request.PageIndex);

            return pagedDataResult;
        }

        public async Task<int> ReminderAppointmentSaveAsync(Appointment entity)
        {
            try
            {
                await _semaphoreSlim.WaitAsync();

                #region Validaciones

                // Validar que la fecha del recordatorio no sea en el pasado
                if (entity.DateStart < DateTime.Now)
                    throw new ArgumentException("La fecha del recordatorio no puede ser en el pasado");

                #endregion

                #region Observer

                // Crear instancia de Notifier<Reminder>
                var reminderNotifier = new Notifier<Reminder>();
                // Crear y adjuntar observadores si es necesario
                // Ejemplo: var emailObserver = new EmailObserver();
                // reminderNotifier.Attach(emailObserver);

                #endregion

                // Guardar el recordatorio en la base de datos
                if (entity.Id == 0)
                    await unitOfWork.AddRepositories.ReminderRepository.AddAsync(entity);
                else
                    unitOfWork.AddRepositories.ReminderRepository.Update(entity);

                await unitOfWork.CompleteAsync();

                reminderNotifier.Notify(entity);
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

        public async Task<bool> DeleteAsync(int id)
        {
            var reminderToDelete = await unitOfWork.AddRepositories.ReminderRepository.FindAsync(id);
            if (reminderToDelete == null)
                throw new KeyNotFoundException("Recordatorio no encontrado");

            unitOfWork.Delete(reminderToDelete);
            await unitOfWork.CompleteAsync();
            return true;
        }
    }
}

