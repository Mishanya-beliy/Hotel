using Hotel.DAL.Entities;
using System.Collections.Generic;

namespace Hotel.BLL.DTO
{
    public class RoomDTO : Room
    {
        public new virtual CategoryDTO Category { set; get; }
        public new virtual ICollection<BookingDTO> Bookings { get; set; }
    }
}
