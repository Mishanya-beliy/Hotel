using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotel.DAL.Entities
{
    public class Guest
    {
        public int ID { set; get; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        //[Required]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        public short? House { get; set; }
        public short? Apartment { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
