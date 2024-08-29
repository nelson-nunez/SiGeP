using SiGeP.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Model.Model
{
    public class Payment : BaseEntity
    {
        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(18, 2)")] 
        public decimal Amount { get; set; }

        public int AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }
    }
}
