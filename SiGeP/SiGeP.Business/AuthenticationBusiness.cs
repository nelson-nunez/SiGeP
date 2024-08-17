using SiGeP.DataAccess.Generic;

namespace SiGeP.Business
{
    public class AuthenticationBusiness
    {
        private readonly UnitOfWork unitOfWork;
        public AuthenticationBusiness(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Authenticate(string userName, string password)
        {
            var users = await unitOfWork.Repositories.AppUserRepository.GetAsync(x => x.Name == userName & x.Password == password);
            if (users.Count() != 1)
                throw new Exception("Credenciales inválidas");
        }
    }
}
