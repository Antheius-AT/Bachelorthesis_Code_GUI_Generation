using Models.Metadata;

namespace Models.UseCases.IncludingUserInteraction.UseCase2
{
    public class Address
    {
        public Address()
        {
            this.City = "Demo city";
            this.ZipCode = 12459;
            this.HouseNumber = "196/Object C/Entrance 5A";
            this.Street = "This is a very very long street name";
        }

        [StringLength(100)]
        [Editable]
        public string Street { get; set; }

        [StringLength(20)]
        [Editable]
        public string City { get; set; }

        [StringLength(10)]
        [Editable]
        public int ZipCode { get; set; }

        [StringLength(10)]
        [Editable]
        public string HouseNumber { get; set; }
    }
}
