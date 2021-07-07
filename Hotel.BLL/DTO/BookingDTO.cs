using System;

namespace Hotel.BLL.DTO
{
    public class BookingDTO
    {
        public int ID { set; get; }
        public int GuestID { set; get; }
        public int RoomID { set; get; }
        public DateTime BookingDate { set; get; }
        public DateTime CheckIn { set; get; }
        public DateTime CkeckOut { set; get; }
        public GuestDTO Guest { set; get; }
        public RoomDTO Room { set; get; }
    }
}
