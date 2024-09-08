using SiGeP.Bussines.Interfaces;
using SiGeP.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Business.Notifiers
{
    public class PaymentObserver : Bussines.Interfaces.IObserver<Appointment>
    {
        public void Update(Appointment appointment)
        {
            // Lógica para crear un pago pendiente asociado a la cita
            var payment = new Payment
            {
                Date = appointment.DateEnd, // Ejemplo: Fecha de pago al finalizar la cita
                Amount = 100m, // Monto del pago
                AppointmentId = appointment.Id
            };

            // Lógica para guardar el pago en la base de datos
            // ...
        }
    }
}
