using System.Collections.Generic;

namespace Hotel.BLL.DTO
{
    public class CategoryDTO
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public int Bed { set; get; }
        public ICollection<PriceDTO> Prices { set; get; }
    }
}
