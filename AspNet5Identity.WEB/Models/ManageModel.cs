using System.ComponentModel.DataAnnotations;

namespace AspNet5Identity.WEB.Models
{
    public class ManageModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "About me")]
        public string AboutMe { get; set; }
    }
}