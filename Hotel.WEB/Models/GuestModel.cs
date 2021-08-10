using Hotel.BLL.DTO;
using Hotel.WEB.Models.Booking;
using System.Collections.Generic;

namespace Hotel.WEB.Models
{
    public class GuestModel : GuestDTO
    {
        public new virtual ICollection<BookingModel> Bookings { set; get; }
        public string FullName
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }
    }
}
