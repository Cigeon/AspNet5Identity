using System.ComponentModel.DataAnnotations;

namespace AspNet5Identity.WEB.Models
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\+?\d+", ErrorMessage = "Invalid Phone number")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "About me")]
        public string AboutMe { get; set; }
    }
}