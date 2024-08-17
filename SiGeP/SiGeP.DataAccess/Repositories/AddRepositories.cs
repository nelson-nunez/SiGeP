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

        #region Repositorios

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

        private AppUserRepository _appUserRepository;
        public AppUserRepository AppUserRepository
        {
            get
            {
                return _appUserRepository ??= new AppUserRepository(_context);
            }
        }

        #endregion

    }
}
