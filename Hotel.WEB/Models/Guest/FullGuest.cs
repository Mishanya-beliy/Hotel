using Hotel.BLL.DTO;
using Hotel.WEB.Models.Booking;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotel.WEB.Models.Guest
{
    public class FullGuest : GuestDTO
    {
        [EmailAddress]
        public new string Email { set; get; }
        public new virtual ICollection<BookingModel> Bookings { get; set; }
    }
}
