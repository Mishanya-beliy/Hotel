using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    public class AccountModel
    {
        public int ID { get; set; }
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
