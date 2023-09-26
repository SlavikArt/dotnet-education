using System.Text;

namespace Receipt
{
    public struct Receipt
    {
        public string CompanyName { get; set; }
        public string ShopName { get; set; }
        public Address Address { get; set; }
        public List<Item> Items { get; set; }
        public string SpecialPromo { get; set; }
        public DateTime TodaysDate { get; set; }
        public double ClientBonus { get; set; }
        public int CheckNumber { get; set; }
        public int CashBoxNumber { get; set; }
        public string ActiveSale { get; set; }
        public string Prediction { get; set; }
        public double BonusGathered { get; set; }
        public double BonusBalance => ClientBonus + BonusGathered;

        private const int TaxPercentage = 10;

        public double Total => SubTotal + Tax;
        public double Tax => SubTotal / TaxPercentage;
        public double SubTotal => Items.Sum(i => i.Total);

        public Receipt(string companyName, string shopName, Address address,
            List<Item> items, string specialPromo, DateTime todaysDate,
            double clientBonus, int checkNumber, int cashBoxNumber, string activeSale, 
            string prediction, double bonusGathered)
        {
            CompanyName = companyName;
            ShopName = shopName;
            Address = address;
            Items = items;
            SpecialPromo = specialPromo;
            TodaysDate = todaysDate;
            ClientBonus = clientBonus;
            CheckNumber = checkNumber;
            CashBoxNumber = cashBoxNumber;
            ActiveSale = activeSale;
            Prediction = prediction;
            BonusGathered = bonusGathered;
        }

        public void AddItem(string itemName, int quantity, double price)
        {
            var item = new Item
            {
                Quantity = quantity,
                Name = itemName,
                Price = price,
            };
            Items.Add(item);
        }
        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            const int width = 40;

            char[] up = new char[width];
            char[] down = new char[width];
            char[] mid = new char[width];
            char[] space = new char[width];

            for (int i = 0; i < width; i++)
            {
                if (i == 0)
                {
                    up[i] = Pos.upLeft;
                    down[i] = Pos.downLeft;
                    mid[i] = Pos.midLeft;
                    space[i] = Pos.spaceLeft;
                }
                else if (i == width - 1)
                {
                    up[i] = Pos.upRight;
                    down[i] = Pos.downRight;
                    mid[i] = Pos.midRight;
                    space[i] = Pos.spaceRight;
                }
                else
                {
                    up[i] = Pos.mid;
                    down[i] = Pos.mid;
                    mid[i] = Pos.mid;
                    space[i] = Pos.mid;
                }
            }

            sb.Append(TextLine(up, width));

            sb.Append(TextLine(space, width));

            sb.Append(CenterTextLine(CompanyName, width));
            sb.Append(CenterTextLine(ShopName, width));
            sb.Append(CenterSpaceTextLine(Address.City, width));
            sb.Append(CenterSpaceTextLine(Address.Street + ", " + Address.House, width));

            sb.Append(TextLine(space, width));

            foreach (var item in Items)
            {
                sb.Append(LeftRightSpaceTextLine(item.Quantity + " X", item.Price.ToString("F2"), width));
                sb.Append(LeftRightSpaceTextLine(item.Name, item.Total.ToString("F2"), width));
                if (item != Items.Last())
                    sb.Append(LeftSpaceTextLine("", width));
            }

            sb.Append(TextLine(mid, width));

            sb.Append(PromoTextLine(SpecialPromo, width));

            sb.Append(TextLine(space, width));

            sb.Append(PromoTextLine("Станом на " + TodaysDate + 
                "\nВи маєте " + ClientBonus.ToString("F2") + " бонусів!", width));

            sb.Append(TextLine(mid, width));

            sb.Append(PromoTextLine("Чек #" + CheckNumber + 
                "\nКаса #" + CashBoxNumber, width));

            sb.Append(TextLine(mid, width));

            sb.Append(CenterTextLine(ActiveSale, width));

            sb.Append(TextLine(mid, width));

            sb.Append(PromoTextLine("Передбачення для Вас:\n" + Prediction, width));

            sb.Append(TextLine(mid, width));

            sb.Append(PromoTextLine("Ви отримали " + BonusGathered.ToString("F2") + " бонусів," +
                "\nтепер у Вас є " + BonusBalance.ToString("F2") + " бонусів!", width));

            sb.Append(TextLine(mid, width));

            sb.Append(LeftRightSpaceTextLine($"ПДВ: {Tax.ToString("F2")} (10%)",
                SubTotal.ToString("F2"), width));

            sb.Append(TextLine(mid, width));

            sb.Append(LeftRightSpaceTextLine("Усього:", Total.ToString("F2"), width));

            sb.Append(TextLine(mid, width));

            sb.Append(CenterTextLine("Дякуємо, приходьте ще!", width));

            sb.Append(TextLine(down, width));

            return sb.ToString();
        }
        static private string CenterTextLine(string text, int width)
        {
            var sb = new StringBuilder();
            int tmp = (int)Math.Round(Math.Ceiling((double)width / 2) - Math.Ceiling((double)text.Length / 2));

            sb.Append('├');
            for (int i = 1; i < tmp; i++)
                sb.Append('─');
            sb.Append(text);

            for (int i = tmp + text.Length; i < width - 1; i++)
                sb.Append('─');
            sb.Append('┤');
            sb.AppendLine();

            return sb.ToString();
        }
        static private string CenterSpaceTextLine(string text, int width)
        {
            var sb = new StringBuilder();
            int tmp = (int)Math.Round(Math.Ceiling((double)width / 2) - Math.Ceiling((double)text.Length / 2));

            sb.Append('│');
            for (int i = 1; i < tmp; i++)
                sb.Append(" ");
            sb.Append(text);

            for (int i = tmp + text.Length; i < width - 1; i++)
                sb.Append(" ");
            sb.Append('│');
            sb.AppendLine();

            return sb.ToString();
        }
        static private string LeftRightSpaceTextLine(string textLeft, string textRight, int width)
        {
            var sb = new StringBuilder();

            sb.Append('│' + textLeft);
            for (int i = textLeft.Length + 1; i < width - textRight.Length - 1; i++)
                sb.Append(" ");
            sb.Append(textRight + '│');
            sb.AppendLine();

            return sb.ToString();
        }
        static private string LeftSpaceTextLine(string text, int width)
        {
            var sb = new StringBuilder();

            sb.Append('│' + text);
            for (int i = text.Length + 1; i < width - 1; i++)
                sb.Append(" ");
            sb.Append('│');
            sb.AppendLine();

            return sb.ToString();
        }
        static private string PromoTextLine(string text, int width)
        {
            int tmp = 0;
            var sb = new StringBuilder();

            sb.Append('│');
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != '\n')
                    sb.Append(text[i]);
                else
                {
                    for (int j = i; j < width - 2; j++)
                    {
                        sb.Append(" ");
                        tmp++;
                    }
                    tmp++;
                    sb.Append('│');
                    sb.AppendLine();
                    sb.Append('│');
                }
            }
            for (int k = 0; k < width * 2 - 2 - text.Length - tmp; k++)
                sb.Append(" ");
            sb.Append('│');
            sb.AppendLine();

            return sb.ToString();
        }
        static private string TextLine(char[] arr, int width)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < width; i++)
                sb.Append(arr[i]);
            sb.AppendLine();

            return sb.ToString();
        }
    }
    public struct Pos
    {
        public const char upLeft = '┌';
        public const char upRight = '┐';
        public const char mid = '─';
        public const char midLeft = '├';
        public const char midRight = '┤';
        public const char downLeft = '└';
        public const char downRight = '┘';
        public const char space = ' ';
        public const char spaceLeft = '│';
        public const char spaceRight = '│';
    }
}
