using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.WEB.Models.Guest
{
    public class ShortGuest
    {
        public int ID { set; get; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        public string City { get; set; }
        public string FullName
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }
    }
}
