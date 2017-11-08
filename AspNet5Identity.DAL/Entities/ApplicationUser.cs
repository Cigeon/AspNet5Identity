using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNet5Identity.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
