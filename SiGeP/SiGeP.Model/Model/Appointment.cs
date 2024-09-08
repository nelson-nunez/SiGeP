using SiGeP.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiGeP.Model.Interfaces;

namespace SiGeP.Model.Model
{
    public class Appointment : BaseEntity
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(256)]
        public string Address { get; set; }
        public AppointmentStatus Status { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual Reminder? Reminder { get; set; }

    }


    public enum AppointmentStatus
    {
        Scheduled= 0,
        Rescheduled = 1,
        Canceled = 2,
        Completed = 3,
    }
}
