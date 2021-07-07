using System;

namespace Hotel.API.Models
{
    public class BookingModel
    {
        public BookingModel()
        {
            BookingDate = DateTime.Now;
        }
        public int ID { set; get; }
        public int GuestID { set; get; }
        public int RoomID { set; get; }
        public DateTime BookingDate { set; get; }
        public DateTime CheckIn { set; get; }
        public DateTime CkeckOut { set; get; }
        public GuestModel Guest { set; get; }
        public RoomModel Room { set; get; }
    }
}
