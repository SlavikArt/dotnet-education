namespace NumberConsoleGraphics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the character to be used for console graphics: ");
            char graphicChar = Console.ReadKey().KeyChar;

            Console.Write("\nEnter the number to be displayed: ");
            int inputNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            PrintNumber(inputNumber, graphicChar);
            Console.WriteLine();
            PrintNumber(inputNumber);
            Console.WriteLine();
        }

        static void PrintNumber(int number)
        {
            NumberPattern pattern = new NumberPattern();

            string numberString = number.ToString();
            for (int i = 0; i < 5; i++)
            {
                foreach (char digitChar in numberString)
                {
                    int digit = int.Parse(digitChar.ToString());
                    Console.Write(pattern[digit][i].Replace('*', digitChar) + "  ");
                }
                Console.WriteLine();
            }
        }
        static void PrintNumber(int number, char graphicChar)
        {
            NumberPattern pattern = new NumberPattern();

            string numberString = number.ToString();
            for (int i = 0; i < 5; i++)
            {
                foreach (char digitChar in numberString)
                {
                    int digit = int.Parse(digitChar.ToString());
                    Console.Write(pattern[digit][i].Replace('*', graphicChar) + "  ");
                }
                Console.WriteLine();
            }
        }
    }
}
