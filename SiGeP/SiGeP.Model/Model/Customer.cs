using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SiGeP.Model.Base;
using SiGeP.Model.ModelUser;

namespace SiGeP.Model.Model
{
    public class Customer : BaseEntity
    {
        [Required]
        public int PersonId { get; set; }
        public virtual Person.Person Person { get; set; }

        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; }

    }
}