using System.ComponentModel.DataAnnotations;

namespace Bookify.Auth.Business.Models.Request
{
    public class RequestLoginModel
    {
        [MaxLength(256, ErrorMessage = "Username is too long.")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Invalid Email.")]
        [Required(ErrorMessage = "Email is mandatory.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is mandatory.")]
        [MaxLength(100, ErrorMessage = "Invalid password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
