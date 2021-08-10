using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.WEB.Models
{
    public class ProfitModel
    {
        [DataType(DataType.Date)]
        public DateTime Start { set; get; }
        [DataType(DataType.Date)]
        public DateTime End { set; get; }
        public int Money { set; get; }
    }
}
