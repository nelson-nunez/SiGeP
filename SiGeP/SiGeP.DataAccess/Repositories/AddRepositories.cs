using SiGeP.Model;

namespace SiGeP.DataAccess.Repositories
{
    public class AddRepositories
    {
        private readonly DbModelContext _context;
        public AddRepositories(DbModelContext context)
        {
            _context = context;
        }

        private AppUserRepository _appUserRepository;
        public AppUserRepository AppUserRepository
        {
            get
            {
                return _appUserRepository ??= new AppUserRepository(_context);
            }
        }

        #region Person

        private GenderRepository _genderRepository;
        public GenderRepository GenderRepository
        {
            get
            {
                return _genderRepository ??= new GenderRepository(_context);
            }
        }

        private CustomerRepository _customerRepository;
        public CustomerRepository CustomerRepository
        {
            get
            {
                return _customerRepository ??= new CustomerRepository(_context);
            }
        }

        
        private DoctorRepository _doctorRepository;
        public DoctorRepository DoctorRepository
        {
            get
            {
                return _doctorRepository ??= new DoctorRepository(_context);
            }
        }

        #endregion

        #region ADDRESS

        private CityRepository _cityRepository;
        public CityRepository CityRepository
        {
            get
            {
                return _cityRepository ??= new CityRepository(_context);
            }
        }
        
        private NeighborhoodRepository _neighborhoodRepository;
        public NeighborhoodRepository NeighborhoodRepository
        {
            get
            {
                return _neighborhoodRepository ??= new NeighborhoodRepository(_context);
            }
        }
        
        private ProvinceRepository _provinceRepository;
        public ProvinceRepository ProvinceRepository
        {
            get
            {
                return _provinceRepository ??= new ProvinceRepository(_context);
            }
        }
        
        private AppointmentRepository _appointmentRepository;
        public AppointmentRepository AppointmentRepository
        {
            get
            {
                return _appointmentRepository ??= new AppointmentRepository(_context);
            }
        }

        #endregion

        private ReminderRepository _reminderRepository;
        public ReminderRepository ReminderRepository
        {
            get
            {
                return _reminderRepository ??= new ReminderRepository(_context);
            }
        }
    }
}
