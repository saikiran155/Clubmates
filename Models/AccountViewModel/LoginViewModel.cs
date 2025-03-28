using System.ComponentModel.DataAnnotations;

namespace ClubMates.Web.Models.AccountViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User Name is mandatory")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage ="Password is mandatory")]
        public string Password { get; set; } = string.Empty;
    }
}
