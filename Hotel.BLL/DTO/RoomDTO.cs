using System.Collections.Generic;

namespace Hotel.BLL.DTO
{
    public class RoomDTO
    {
        public int ID { set; get; }
        public int CategoryID { set; get; }
        public string Name { set; get; }
        public CategoryDTO Category { set; get; }
        public ICollection<BookingDTO> Bookings { get; set; }
    }
}
