using SiGeP.Business.Base;
using SiGeP.Business.Notifiers;
using SiGeP.DataAccess.Generic;
using SiGeP.Model.Base;
using SiGeP.Model.Model;
using System.Linq.Expressions;

namespace SiGeP.Business
{
    public class AppointmentBusiness: BusinessBase<Appointment>
    {
        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public AppointmentBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<PagedDataResponse<Appointment>> GetPagedResultAsync(PagingSortFilterRequest request)
        {
            IEnumerable<Appointment> list = new List<Appointment>();
            Expression<Func<Appointment, bool>> filter = x => true;

            // Añadir filtros específicos aquí si es necesario
            var pagedDataResult = await unitOfWork.AddRepositories.GetRepository<Appointment>().GetPagedResultAsync(
                request.FilterBy, request.FilterValue, filter, request.OrderBy, request.PageSize, request.PageIndex);

            return pagedDataResult;
        }

        public async Task<int> SaveAsync(Appointment entity)
        {
            try
            {
                await _semaphoreSlim.WaitAsync();

                #region Validaciones

                // Validar que el turno no exceda las 3 horas
                if (entity.DateEnd - entity.DateStart > TimeSpan.FromHours(3))
                    throw new ArgumentException("El turno no puede exceder las 3 horas");

                // Verificar que no exista otro turno en el mismo intervalo de tiempo 
                var existe = await unitOfWork.AddRepositories.GetRepository<Appointment>()
                    .GetAsync(x => (entity.DateStart >= x.DateStart && entity.DateStart < x.DateEnd) ||
                                   (entity.DateEnd > x.DateStart && entity.DateEnd <= x.DateEnd) ||
                                   (entity.DateStart <= x.DateStart && entity.DateEnd >= x.DateEnd));
                if (existe.Any())
                    throw new InvalidOperationException("Ya existen turnos asignados en la fecha y hora indicada");

                #endregion

                #region Observer

                //// Crear instancia de Notifier<Appointment>
                //var appointmentNotifier = new Notifier<Appointment>();

                //// Crear y adjuntar observadores
                //var reminderObserver = new ReminderObserver();
                //var paymentObserver = new PaymentObserver();

                //appointmentNotifier.Attach(reminderObserver);
                ////appointmentNotifier.Attach(paymentObserver);

                #endregion

                // Guardar la cita en la base de datos
                if (entity.Id == 0)
                    await unitOfWork.AddRepositories.GetRepository<Appointment>().AddAsync(entity);
                else
                     unitOfWork.AddRepositories.GetRepository<Appointment>().Update(entity);

                await unitOfWork.CompleteAsync();

                // Notificar a los observadores después de la creación/actualización
                //appointmentNotifier.Notify(entity);

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
