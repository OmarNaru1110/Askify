using System.ComponentModel.DataAnnotations;

namespace Askify.ViewModels
{
    public class UserRegisterationVM
    {
        [Required]
        public string Username { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [DataType(DataType.Password), Required]
        public string Password { get; set; }
        [DataType(DataType.Password), Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
