using System;

namespace Hotel.DAL.Entities
{
    public class Booking
    {
        public int ID { set; get; }
        public int GuestID { set; get; }
        public int RoomID { set; get; }
        public DateTime BookingDate { set; get; }
        public DateTime CheckIn { set; get; }
        public DateTime CkeckOut { set; get; }
        public Guest Guest { set; get; }
        public Room Room { set; get; }

    }
}
