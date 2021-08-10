using Hotel.DAL.Entities;
using System.Collections.Generic;

namespace Hotel.BLL.DTO
{
    public class CategoryDTO : Category
    {

        public new virtual ICollection<PriceDTO> Prices { set; get; }
    }
}
