using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.UseCases.DisplayOnly.UseCase2
{
    public class Address
    {
        public Address()
        {
            this.City = "Demo city";
            this.Zipcode = 12459;
            this.HouseNumber = "196/Object C/Entrance 5A";
            this.Street = "Research street 24";
        }

        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public int Zipcode { get; set; }

        public string HouseNumber { get; set; }
    }
}
