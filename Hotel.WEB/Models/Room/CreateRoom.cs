using System.ComponentModel.DataAnnotations;

namespace Hotel.WEB.Models.Room
{
    public class CreateRoom
    {        
        public int ID { set; get; }
        [Display(Name = "Room name")]
        public string Name { set; get; }
        public virtual CategoryModel Category { set; get; }
        public int CategoryID { set; get; }
    }
}
