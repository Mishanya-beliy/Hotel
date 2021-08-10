using Microsoft.AspNetCore.Identity;

namespace Hotel.WEB.Models.Security
{
    public class HotelUser : IdentityUser<string>
    {
        public int GuestID { set; get; }
    }
}
