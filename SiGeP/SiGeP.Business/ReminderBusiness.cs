using SiGeP.Business.Base;
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
    public class ReminderBusiness: BusinessBase<Reminder>
    {
        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public ReminderBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {
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
                //if (entity.Id == 0)
                //    await unitOfWork.AddRepositories.GetRepository<Appointment>().AddAsync(entity);
                //else
                //    unitOfWork.AddRepositories.GetRepository<Appointment>().Update(entity);

                //await unitOfWork.CompleteAsync();

                //reminderNotifier.Notify(entity);
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

