using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.BLL.DTO
{
    public class ConfirmBookingDTO
    {
        public ConfirmBookingDTO()
        { }

        public ConfirmBookingDTO(string name, string roomName, int coast, DateTime checkIn, DateTime checkOut)
        {
            Name = name;
            RoomName = roomName;
            Coast = coast;
            CheckIn = checkIn;
            CheckOut = checkOut;
        }

        public string Name { set; get; }
        public string RoomName { set; get; }

        [DataType(DataType.Currency)]
        public int Coast { set; get; }

        public DateTime CheckIn { set; get; }
        public DateTime CheckOut { set; get; }
    }
}
