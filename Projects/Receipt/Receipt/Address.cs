namespace Receipt
{
    public struct Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string House { get; set; }

        public Address(string street, string city, string house)
        {
            Street = street;
            City = city;
            House = house;
        }
    }
}
