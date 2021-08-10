using Hotel.BLL.DTO;

namespace Hotel.WEB.Models.Booking
{
    public class ConfirmBooking : ConfirmBookingDTO
    {
        public virtual GuestModel Guest { set; get; }
        public virtual RoomModel Room { set; get; }

    }
}
