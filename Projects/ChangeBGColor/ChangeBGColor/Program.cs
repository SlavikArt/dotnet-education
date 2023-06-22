while (true)
{
    ConsoleKeyInfo key = Console.ReadKey();

    switch (key.Key)
    {
        case ConsoleKey.D0:
            Console.BackgroundColor = ConsoleColor.Magenta;
            break;
        case ConsoleKey.D1:
            Console.BackgroundColor = ConsoleColor.Red;
            break;
        case ConsoleKey.D2:
            Console.BackgroundColor = ConsoleColor.Yellow;
            break;
        case ConsoleKey.D3:
            Console.BackgroundColor = ConsoleColor.Green;
            break;
        case ConsoleKey.D4:
            Console.BackgroundColor = ConsoleColor.Cyan;
            break;
        case ConsoleKey.D5:
            Console.BackgroundColor = ConsoleColor.Blue;
            break;
        case ConsoleKey.D6:
            Console.BackgroundColor = ConsoleColor.White;
            break;
        case ConsoleKey.D7:
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            break;
        case ConsoleKey.D8:
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            break;
        case ConsoleKey.D9:
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            break;
        default:
            Console.BackgroundColor = ConsoleColor.Black;
            break;
    }
    Console.Clear();
}
