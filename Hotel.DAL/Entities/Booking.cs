using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.DAL.Entities
{
    public class Booking
    {
        public Booking()
        {
            BookingDate = DateTime.Now;
        }
        public int ID { set; get; }

        [Required]
        public int GuestID { set; get; }

        [Required]
        public int RoomID { set; get; }


        [Required]
        public DateTime CheckIn { set; get; }
        [Required]
        public DateTime CkeckOut { set; get; }
        public DateTime BookingDate { set; get; } = DateTime.Now;

        public virtual Guest Guest { set; get; }
        public virtual Room Room { set; get; }

    }
}
