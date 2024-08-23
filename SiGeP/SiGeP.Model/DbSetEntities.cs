using Microsoft.EntityFrameworkCore;
using SiGeP.Model.Model;
using SiGeP.Model.Model.Address;
using SiGeP.Model.Model.Person;
using SiGeP.Model.ModelUser;

namespace SiGeP.Model
{
    public class DbSetEntities
    {
        public DbSet<Address> Address { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Neighborhood> Neighborhood { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<MedicalRecord> MedicalRecord { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Reminder> Reminder { get; set; }
    }
}
