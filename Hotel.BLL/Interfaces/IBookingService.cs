using Hotel.BLL.DTO;
using System.Collections.Generic;

namespace Hotel.BLL.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<BookingDTO> GetAllBookings();
        BookingDTO Get(int id);
        public void Booking(BookingDTO booking);
    }
}
