using Hotel.DAL.Entities;

namespace Hotel.BLL.DTO
{
    public class BookingDTO : Booking
    {
        public new virtual GuestDTO Guest { set; get; }
        public new virtual  RoomDTO Room { set; get; }
    }
}
