using System.Collections.Generic;

namespace Hotel.DAL.Entities
{
    public class Room
    {        
        public int ID { set; get; }
        public int CategoryID { set; get; }
        public string Name { set; get; }
        public virtual Category Category { set; get; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
