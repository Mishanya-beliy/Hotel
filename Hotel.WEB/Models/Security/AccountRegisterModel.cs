using System.ComponentModel.DataAnnotations;

namespace Hotel.WEB.Models.Security
{
    public class AccountRegisterModel : AccountModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
