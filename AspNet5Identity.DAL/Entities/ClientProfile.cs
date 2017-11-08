using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNet5Identity.DAL.Entities
{
    public class ClientProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AboutMe { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
