using Hotel.DAL.Entities;
using System.Collections.Generic;

namespace Hotel.WEB.Models
{
    public class CategoryModel
    {
        public int ID { set; get; }

        public Categories Name { set; get; }
        public int Bed { set; get; }
        public int MaxPeople { set; get; }

        public virtual ICollection<PriceModel> Prices { set; get; }
    }
}