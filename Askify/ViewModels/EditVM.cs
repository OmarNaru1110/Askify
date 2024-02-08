using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Askify.ViewModels
{
    public class EditVM
    {
        [Required]
        public string UserName { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
