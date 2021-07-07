using System.Collections.Generic;

namespace Hotel.DAL.Entities
{
    public class Room
    {        
        public int ID { set; get; }
        public int CategoryID { set; get; }
        public string Name { set; get; }
        public Category Category { set; get; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
