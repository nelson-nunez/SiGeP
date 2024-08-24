using SiGeP.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Model.DTO
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Address { get; set; }
        public AppointmentStatus Status { get; set; }
        public int CustomerId { get; set; }
        public int PaymentId { get; set; }
        public int ReminderId { get; set; } 
    }
}
