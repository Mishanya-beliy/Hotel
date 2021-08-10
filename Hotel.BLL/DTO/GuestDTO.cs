using Hotel.DAL.Entities;
using System.Collections.Generic;

namespace Hotel.BLL.DTO
{
    public class GuestDTO : Guest
    {        
        public new virtual ICollection<BookingDTO> Bookings { get; set; }
    }
}
