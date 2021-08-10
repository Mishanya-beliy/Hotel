using Hotel.BLL.DTO;
using System.Collections.Generic;

namespace Hotel.BLL.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<BookingDTO> GetAllBookings();
        BookingDTO Get(int id);
        public ConfirmBookingDTO Booking(BookingDTO booking);
        bool Delete(int id);
        bool Update(int id, BookingDTO bookingDTO);
    }
}
