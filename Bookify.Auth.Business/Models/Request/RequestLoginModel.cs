using System.ComponentModel.DataAnnotations;

namespace Bookify.Auth.Business.Models.Request
{
    public class RequestLoginModel
    {
        [MaxLength(256, ErrorMessage = "Username is too long.")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Invalid username.")]
        [Required(ErrorMessage = "Username is mandatory.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is mandatory.")]
        [MaxLength(100, ErrorMessage = "Invalid password")]
        public string Password { get; set; }
    }
}
