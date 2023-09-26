namespace Delegates
{
    class Program
    {
        delegate void Menu();
        static void Main(string[] args)
        {
            Menu[] menuOptions = new Menu[]
            {
                NewGame,
                LoadGame,
                GameRules,
                AboutAuthor,
                Exit
            };

            while (true)
            {
                Console.Clear();

                Console.WriteLine("1 - новая игра");
                Console.WriteLine("2 - загрузить игру");
                Console.WriteLine("3 - правила");
                Console.WriteLine("4 - об авторе");
                Console.WriteLine("@ - выход");

                Console.Write("\n>>> ");
                string input = Console.ReadLine();
                int index;

                if (input == "@")
                    index = 5;
                else if (!int.TryParse(input, out index) || index < 1 || index > 4)
                {
                    Console.WriteLine("Неверный ввод. Введите число от 1 до 4 или @ для выхода.");
                    continue;
                }
                menuOptions[index - 1].Invoke();
                Thread.Sleep(3500);
            }
        }

        static void NewGame()
        {
            Console.WriteLine("Новая игра. Первому игроку приготовиться!");
        }
        static void LoadGame()
        {
            Console.WriteLine("Загрузить игру. Выберите свой сценарий.\n" +
                "И не забывайте, что ваше решение может изменить ход истории.");
        }
        static void GameRules()
        {
            Console.WriteLine("Правила. Они есть, но вы можете их нарушать.\n" +
                "Главное, чтобы вас не поймали.\n" +
                "И не доверяйте никому, кроме себя.");
        }
        static void AboutAuthor()
        {
            Console.WriteLine("Об авторе. It's-a me, Mario!");
        }
        static void Exit()
        {
            Console.WriteLine("Выход. Вы уверены, что хотите закончить эту игру?\n" +
                "Вы не хотите попробовать другие варианты?");
            Environment.Exit(0);
        }
    }
}
