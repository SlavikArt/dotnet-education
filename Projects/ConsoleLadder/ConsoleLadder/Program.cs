Console.Write("Введите количество ступенек: ");
int steps = Convert.ToInt32(Console.ReadLine());

int offset = 0;

for (int i = 0; i < steps; i++)
{
    for (int j = 0; j < offset; j++)
        Console.Write(" ");

    for (int j = 0; j < 3; j++)
        Console.Write("*");

    Console.WriteLine();

    for (int j = 0; j < offset + 2; j++)
        Console.Write(" ");
    Console.WriteLine("*");

    offset += 3;
}

for (int j = 0; j < offset; j++)
    Console.Write(" ");
Console.WriteLine("***");
