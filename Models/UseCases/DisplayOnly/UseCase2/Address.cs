using Models.Metadata;

namespace Models.UseCases.DisplayOnly.UseCase2
{
    public class Address
    {
        public Address()
        {
            this.City = "Demo city";
            this.Zipcode = 12459;
            this.HouseNumber = "196/Object C/Entrance 5A";
            this.Street = "This is a very very long street name";
        }

        [StringLength(100)]
        public string Street { get; set; }

        [StringLength(20)]
        public string City { get; set; }

        [StringLength(10)]
        public int Zipcode { get; set; }

        [StringLength(10)]
        public string HouseNumber { get; set; }
    }
}
