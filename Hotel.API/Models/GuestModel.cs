using System.Collections.Generic;

namespace Hotel.API.Models
{
    public class GuestModel
    {
        public int ID { set; get; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public ICollection<BookingModel> Bookings { get; set; }
    }
}
