using System.Collections.Generic;

namespace Hotel.API.Models
{
    public class RoomModel
    {
        public int ID { set; get; }
        public int CategoryID { set; get; }
        public string Name { set; get; }
        public CategoryModel Category { set; get; }
        public ICollection<BookingModel> Bookings { get; set; }
    }
}
