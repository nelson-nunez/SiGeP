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
    public class Appointment : BaseEntity
    {
        public DateTime Date { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(256)]
        public string Address { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual Reminder Reminder { get; set; }
    }
}
