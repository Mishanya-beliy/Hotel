using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    public class RegisterModel
    {
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
