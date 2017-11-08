using AspNet5Identity.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace AspNet5Identity.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {
        }
    }
}
