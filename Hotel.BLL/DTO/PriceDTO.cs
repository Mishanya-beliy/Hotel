using Hotel.DAL.Entities;

namespace Hotel.BLL.DTO
{
    public class PriceDTO : Price
    {
        public virtual  CategoryDTO Category { set; get; }
    }
}
