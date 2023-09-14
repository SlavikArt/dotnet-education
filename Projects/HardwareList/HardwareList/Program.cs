abstract class DataStorage
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }

    public abstract void Print();
    public virtual void Input()
    {
        Console.Write("Enter Name: ");
        Name = Console.ReadLine();
        Console.Write("Enter Manufacturer: ");
        Manufacturer = Console.ReadLine();
        Console.Write("Enter Model: ");
        Model = Console.ReadLine();
        Console.Write("Enter Quantity: ");
        Quantity = int.Parse(Console.ReadLine());
        Console.Write("Enter Price: ");
        Price = double.Parse(Console.ReadLine());
    }
}

class FlashMemory : DataStorage
{
    public double MemorySize { get; set; }
    public double UsbSpeed { get; set; }

    public override void Print()
    {
        Console.WriteLine($"Flash Memory:\n" +
            $"  Name: {Name}\n" +
            $"  Manufacturer: {Manufacturer}\n" +
            $"  Model: {Model}\n" +
            $"  Quantity: {Quantity}\n" +
            $"  Price: {Price}\n" +
            $"  Memory Size: {MemorySize}\n" +
            $"  USB Speed: {UsbSpeed}");
    }
    public override void Input()
    {
        base.Input();
        Console.Write("Enter Memory Size: ");
        MemorySize = double.Parse(Console.ReadLine());
        Console.Write("Enter USB Speed: ");
        UsbSpeed = double.Parse(Console.ReadLine());
    }

}

class HDD : DataStorage
{
    public double DiskSize { get; set; }
    public double UsbSpeed { get; set; }

    public override void Print()
    {
        Console.WriteLine($"HDD:\n" +
            $"  Name: {Name}\n" +
            $"  Manufacturer: {Manufacturer}\n" +
            $"  Model: {Model}\n" +
            $"  Quantity: {Quantity}\n" +
            $"  Price: {Price}\n" +
            $"  Disk Size: {DiskSize}\n" +
            $"  USB Speed: {UsbSpeed}");
    }
    public override void Input()
    {
        base.Input();
        Console.Write("Enter Disk Size: ");
        DiskSize = double.Parse(Console.ReadLine());
        Console.Write("Enter USB Speed: ");
        UsbSpeed = double.Parse(Console.ReadLine());
    }
}

class DVD : DataStorage
{
    public double ReadSpeed { get; set; }
    public double WriteSpeed { get; set; }

    public override void Print()
    {
        Console.WriteLine($"DVD:\n" +
            $"  Name: {Name}\n" +
            $"  Manufacturer: {Manufacturer}\n" +
            $"  Model: {Model}\n" +
            $"  Quantity: {Quantity}\n" +
            $"  Price: {Price}\n" +
            $"  Read Speed: {ReadSpeed}\n" +
            $"  Write Speed: {WriteSpeed}");
    }
    public override void Input()
    {
        base.Input();
        Console.Write("Enter Read Speed: ");
        ReadSpeed = double.Parse(Console.ReadLine());
        Console.Write("Enter Write Speed: ");
        WriteSpeed = double.Parse(Console.ReadLine());
    }
}

class HardwareList
{
    public List<DataStorage> DevicesList { get; set; }

    public HardwareList()
    {
        DevicesList = new List<DataStorage>();
    }

    public int Count
    {
        get { return DevicesList.Count; }
    }

    public void Add(DataStorage storage)
    {
        DevicesList.Add(storage);
    }

    public void RemoveAt(int index)
    {
        DevicesList.RemoveAt(index);
    }

    public void Print()
    {
        foreach (var device in DevicesList)
            device.Print();
    }

    public void Edit(int index)
    {
        var device = DevicesList[index];
        char ch;

        do
        {
            Console.Clear();

            Console.WriteLine("Enter characteristic to edit:\n" +
                "1 - Name\n" +
                "2 - Manufacturer\n" +
                "3 - Model\n" +
                "4 - Quantity\n" +
                "5 - Price\n");

            Console.Write(">>> ");

            string input = Console.ReadLine();
            ch = input.Length > 0 ? input[0] : ' ';

        } while (!(char.IsDigit(ch) && ch >= '1' && ch <= '5'));

        switch (ch)
        {
            case '1':
                Console.WriteLine("Enter a new Name:\n");
                Console.Write(">>> ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newName))
                {
                    device.Name = newName;
                }
                break;
            case '2':
                Console.WriteLine("Enter a new Manufacturer:\n");
                Console.Write(">>> ");
                string newManufacturer = Console.ReadLine();
                if (!string.IsNullOrEmpty(newManufacturer))
                {
                    device.Manufacturer = newManufacturer;
                }
                break;
            case '3':
                Console.WriteLine("Enter a new Model:\n");
                Console.Write(">>> ");
                string newModel = Console.ReadLine();
                if (!string.IsNullOrEmpty(newModel))
                {
                    device.Model = newModel;
                }
                break;
            case '4':
                Console.WriteLine("Enter a new Quantity:\n");
                Console.Write(">>> ");
                string newQuantity = Console.ReadLine();
                int quantity;
                if (!string.IsNullOrEmpty(newQuantity) && Int32.TryParse(newQuantity, out quantity))
                {
                    device.Quantity = quantity;
                }
                break;
            case '5':
                Console.WriteLine("Enter a new Price:\n");
                Console.Write(">>> ");
                string newPrice = Console.ReadLine();
                double price;
                if (!string.IsNullOrEmpty(newPrice) && Double.TryParse(newPrice, out price))
                {
                    device.Price = price;
                }
                break;
        }
        Console.WriteLine("Successfully modified!");
        Thread.Sleep(1000);
    }

    public List<DataStorage> FindAll(Predicate<DataStorage> match)
    {
        return DevicesList.FindAll(match);
    }
}

class Program
{
    static void Main(string[] args)
    {
        HardwareList devicesList = new HardwareList();

        devicesList.Add(new FlashMemory()
        {
            Name = "Flash 1",
            Manufacturer = "Manufacturer 1",
            Model = "Model 1",
            Quantity = 10,
            Price = 1000.0,
            MemorySize = 32.0,
            UsbSpeed = 3.0
        });

        while (true)
        {
            char choice;

            do
            {
                Console.Clear();

                Console.WriteLine("| Hardvare List Manager |\n");

                Console.WriteLine("1 - Add a new device.");
                Console.WriteLine("2 - Remove a device.");
                Console.WriteLine("3 - Print devices list.");
                Console.WriteLine("4 - Search for a device.");
                Console.WriteLine("5 - Change device characteristics.\n");

                Console.Write(">>> ");

                string input = Console.ReadLine();
                choice = input.Length > 0 ? input[0] : ' ';

            } while (!(char.IsDigit(choice) && choice >= '1' && choice <= '5'));

            switch (choice) 
            {
                case '1':
                    char ch1;

                    do
                    {
                        Console.Clear();

                        Console.WriteLine("Choose device type:\n");

                        Console.WriteLine("1 - Flash Memory.");
                        Console.WriteLine("2 - HDD.");
                        Console.WriteLine("3 - DVD.\n");

                        Console.Write(">>> ");

                        string input = Console.ReadLine();
                        ch1 = input.Length > 0 ? input[0] : ' ';

                    } while (!(char.IsDigit(ch1) && ch1 >= '1' && ch1 <= '3'));

                    switch (ch1)
                    {
                        case '1':
                            FlashMemory flashMemory = new FlashMemory();
                            flashMemory.Input();

                            devicesList.Add(flashMemory);

                            Console.WriteLine("\nAdded!");
                            Thread.Sleep(1000);
                            break;
                        case '2':
                            HDD hdd = new HDD();
                            hdd.Input();

                            devicesList.Add(hdd);

                            Console.WriteLine("\nAdded!");
                            Thread.Sleep(1000);
                            break;
                        case '3':
                            DVD dvd = new DVD();
                            dvd.Input();

                            devicesList.Add(dvd);

                            Console.WriteLine("\nAdded!");
                            Thread.Sleep(1000);
                            break;
                    }
                    break;
                case '2':
                    char ch2;

                    do
                    {
                        Console.Clear();

                        Console.WriteLine("Choose device to delete:\n");

                        Console.WriteLine("0 - Return back.");

                        for (int i = 0; i < devicesList.Count; i++)
                            Console.WriteLine((i + 1) + " - " + devicesList.DevicesList[i].Name);
                        Console.Write("\n>>> ");

                        string input = Console.ReadLine();
                        ch2 = input.Length > 0 ? input[0] : ' ';

                    } while (!(char.IsDigit(ch2) && int.Parse(ch2.ToString()) >= 0 && int.Parse(ch2.ToString()) <= devicesList.Count));

                    if (ch2 != '0')
                    {
                        devicesList.RemoveAt(int.Parse(ch2.ToString()) - 1);

                        Console.WriteLine("\nRemoved!");
                        Thread.Sleep(1000);
                    }
                    break;
                case '3':
                    if (devicesList.Count >= 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Devices:\n");

                        devicesList.Print();

                        Console.WriteLine("\nEnter any char to go back.");
                        Console.Write(">>> ");

                        int ch3 = int.Parse(Console.ReadLine());
                    }
                    else
                    {
                        Console.WriteLine("There are no devices in the list");
                        Thread.Sleep(1000);
                    }
                    break;
                case '4':
                    char ch4;

                    do
                    { 
                        Console.Clear();

                        Console.WriteLine("Search by:\n" +
                            "1 - Name\n" +
                            "2 - Manufacturer\n" +
                            "3 - Model\n" +
                            "0 - Return back\n");

                        Console.Write(">>> ");

                        string input = Console.ReadLine();
                        ch4 = input.Length > 0 ? input[0] : ' ';

                    } while (!(char.IsDigit(ch4) && ch4 >= '0' && ch4 <= '3')) ;

                    if (ch4 != '0')
                    {
                        Console.WriteLine("\nEnter search query:\n");

                        Console.Write(">>> ");

                        string query = Console.ReadLine();

                        List<DataStorage> found = new List<DataStorage>();

                        switch (ch4)
                        {
                            case '1':
                                found = devicesList.FindAll(x => x.Name.ToLower() == query.ToLower());
                                break;
                            case '2':
                                found = devicesList.FindAll(x => x.Manufacturer.ToLower() == query.ToLower());
                                break;
                            case '3':
                                found = devicesList.FindAll(x => x.Model.ToLower() == query.ToLower());
                                break;
                        }

                        if (found.Count > 0)
                        {
                            Console.WriteLine("\nFound:\n");
                            foreach (var device in found)
                                device.Print();

                            Console.WriteLine("\nEnter any char to go back.");
                            Console.Write(">>> ");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Device not found");
                            Thread.Sleep(3000);
                        }
                    }
                    break;
                case '5':
                    char ch5;

                    do
                    {
                        Console.Clear();

                        Console.WriteLine("Choose device to edit:\n");

                        Console.WriteLine("0 - Return back.");

                        for (int i = 0; i < devicesList.Count; i++)
                            Console.WriteLine((i + 1) + " - " + devicesList.DevicesList[i].Name);
                        Console.Write("\n>>> ");

                        string input = Console.ReadLine();
                        ch5 = input.Length > 0 ? input[0] : ' ';

                    } while (!(char.IsDigit(ch5) && int.Parse(ch5.ToString()) >= 0 && int.Parse(ch5.ToString()) <= devicesList.Count));

                    if (ch5 != '0')
                        devicesList.Edit(int.Parse(ch5.ToString()) - 1);
                    break;
            }
        }
    }
}
