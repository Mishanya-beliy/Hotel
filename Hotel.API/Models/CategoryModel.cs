using System.Collections.Generic;

namespace Hotel.API.Models
{
    public class CategoryModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public int Bed { set; get; }
        public ICollection<PriceModel> Prices { set; get; }
    }
}
