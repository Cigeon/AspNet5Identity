using AspNet5Identity.BLL.Interfaces;
using AspNet5Identity.DAL.Repositories;

namespace AspNet5Identity.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitOfWork(connection));
        }
    }
}
