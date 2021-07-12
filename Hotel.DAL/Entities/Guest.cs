using System;
using System.Collections.Generic;

namespace Hotel.DAL.Entities
{
    public class Guest
    {
        public Guest()
        {
            Bookings = new HashSet<Booking>();
        }
        public int ID { set; get; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public short? House { get; set; }
        public short? Apartment { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
