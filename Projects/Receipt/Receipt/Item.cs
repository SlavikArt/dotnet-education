namespace Receipt
{
    public struct Item
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public double Total => Quantity * Price;

        public Item(string name, int quantity, double price) 
        {
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public static bool operator ==(Item a, Item b)
        {
            return a.Name == b.Name && a.Quantity == b.Quantity && a.Price == b.Price;
        }

        public static bool operator !=(Item a, Item b)
        {
            return !(a == b);
        }
    }
}
