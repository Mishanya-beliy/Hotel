using Hotel.BLL.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.WEB.Models.Booking
{
    public class BookingModel : BookingDTO
    {
        [DataType(DataType.Date)]
        public new DateTime BookingDate { set; get; }
        [DataType(DataType.Date)]
        public new DateTime CheckIn { set; get; }
        [DataType(DataType.Date)]
        public new  DateTime CkeckOut { set; get; }
        public new virtual GuestModel Guest { set; get; }
        public new virtual RoomModel Room { set; get; }
    }
}
