using System;
using System.Collections.Generic;

namespace Hotel.BLL.DTO
{
    public class GuestDTO
    {
        public int ID { set; get; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public short? House { get; set; }
        public short? Apartment { get; set; }
        public ICollection<BookingDTO> Bookings { get; set; }
    }
}
