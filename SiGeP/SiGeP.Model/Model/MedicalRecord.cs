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
    public class MedicalRecord : BaseEntity
    {
        public DateTime Date { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(256)]
        public string Diagnosis { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(512)]
        public string Treatment { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
