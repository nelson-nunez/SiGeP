using SiGeP.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Model.Model
{
    public class Doctor : BaseEntity
    {

        [Required]
        public int PersonId { get; set; }
        public virtual Person.Person Person { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(128)]
        public string Specialty { get; set; }
    }
}
