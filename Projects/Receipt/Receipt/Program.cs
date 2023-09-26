using System.Text;

namespace Receipt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Address address = new Address 
            { 
                City = "Луцьк",
                Street = "пр. Вол 1",
                House = "буд 1" 
            };
            List<Item> items = new List<Item>
            {
                new Item("Апельсин", 3, 5.05),
                new Item("Груша", 5, 3.4),
                new Item("Яблуко", 2, 2.5)
            };

            DateTime todaysDate = DateTime.Now;

            Receipt receipt = new Receipt(
                companyName: "Сільпо",
                shopName: "Магазин Сільпо",
                address: address,
                items: items,
                specialPromo: "Знижка 50% на банани:\nтільки до 17.09.2023!",
                todaysDate: todaysDate,
                clientBonus: 93.3,
                checkNumber: 2142324324,
                cashBoxNumber: 3,
                activeSale: "Діє акція: Ціна тижня!",
                prediction: "Все буде гаразд ;)",
                bonusGathered: 23.9
            );

            receipt.AddItem("Грейпфрут", 1, 6.03);

            Console.WriteLine(receipt);
        }
    }
}
