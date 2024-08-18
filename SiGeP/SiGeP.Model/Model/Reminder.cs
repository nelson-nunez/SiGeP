using SiGeP.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Model.Model
{
    public class Reminder : BaseEntity
    {
        public DateTime Date { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(16)]
        public string SendMode { get; set; } // "Email" or "SMS", for example

        public bool Sent { get; set; }

        public int AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }
    }

    public enum SendModeType
    {
        Email,
        SMS
    }
}
