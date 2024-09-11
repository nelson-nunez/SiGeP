using SiGeP.Model;
using SiGeP.Model.Model.Address;
using SiGeP.Model.Model;
using SiGeP.Model.ModelUser;
using SiGeP.DataAccess.Generic;

namespace SiGeP.DataAccess.Repositories
{
    public class AddRepositories
    {
        private readonly DbModelContext _context;

        // Diccionario para almacenar las instancias de los repositorios
        private readonly Dictionary<Type, object> _repositories;

        public AddRepositories(DbModelContext context)
        {
            _context = context;

            // ACA DEFINIR LOS REPOSITORIOS <-----------------------------------------------------------------------
            _repositories = new Dictionary<Type, object>
            {
                //Usuarios
                { typeof(AppUser), new AppUserRepository(_context) },
                //Persona
                { typeof(Gender), new GenderRepository(_context) },
                { typeof(Customer), new CustomerRepository(_context) },
                { typeof(Doctor), new DoctorRepository(_context) },
                //Address
                { typeof(City), new CityRepository(_context) },
                { typeof(Neighborhood), new NeighborhoodRepository(_context) },
                { typeof(Province), new ProvinceRepository(_context) },
                //Turnos
                { typeof(Appointment), new AppointmentRepository(_context) },
                { typeof(Reminder), new ReminderRepository(_context) }
            };
        }

        // Método genérico GetRepository para obtener el repositorio basado en el tipo
        public GenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);
            if (_repositories.TryGetValue(type, out var repository))
            {
                return repository as GenericRepository<TEntity>;
            }
            throw new NotSupportedException($"Repository for type {type.Name} not found.");
        }
    }
}
