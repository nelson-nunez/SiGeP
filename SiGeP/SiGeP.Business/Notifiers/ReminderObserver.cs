using SiGeP.Bussines.Interfaces;
using SiGeP.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Business.Notifiers
{
    public class ReminderObserver : Bussines.Interfaces.IObserver<Appointment>
    {
        private readonly ReminderBusiness _reminderBusiness;

        public ReminderObserver(ReminderBusiness reminderBusiness)
        {
            _reminderBusiness = reminderBusiness;
        }

        public void Update(Appointment appointment)
        {
            // Delegar la creación del recordatorio a ReminderBusiness
            _reminderBusiness.ReminderAppointmentSaveAsync(appointment);
        }
    }
}
