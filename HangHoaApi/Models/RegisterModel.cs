using System.ComponentModel.DataAnnotations;

namespace HangHoaApi.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Username is required")]
        public string Username { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "UserName is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PassWord is required")]
        public string Password { get; set; }


       
    }
}
