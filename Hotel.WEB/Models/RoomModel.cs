using System.ComponentModel.DataAnnotations;

namespace Hotel.WEB.Models
{
    public class RoomModel
    {
        public int ID { set; get; }
        [Display(Name = "Room name")]
        public string Name { set; get; }
        public virtual CategoryModel Category { set; get; }
    }
}
