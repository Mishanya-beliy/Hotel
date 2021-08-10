using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.WEB.Models
{
    public class BookingCreateModel
    {
        public BookingCreateModel(int room, DateTime from, DateTime to)
        {
            RoomID = room;
            CheckIn = from;
            CkeckOut = to;

        }
        public BookingCreateModel()
        {

        }
        public int GuestID { set; get; }
        public int RoomID { set; get; }
        public DateTime CheckIn { set; get; }
        public DateTime CkeckOut { set; get; }
        [DataType(DataType.Date)]
        public DateTime BookingDate { set; get; } = DateTime.Now;
    }
}
