Console.Write("Введите количество строк (N): ");
int N = Convert.ToInt32(Console.ReadLine());

Console.Write("Введите количество столбцов (M): ");
int M = Convert.ToInt32(Console.ReadLine());

Console.WriteLine();

int[,] arr = new int[N, M];
int cnt = 1;

for (int i = 0; i < N; i++)
{
    if (i % 2 == 0)
        for (int j = 0; j < M; j++)
            arr[i, j] = cnt++;
    else
        for (int j = M - 1; j >= 0; j--)
            arr[i, j] = cnt++;
}

for (int i = 0; i < N; i++)
{
    for (int j = 0; j < M; j++)
        Console.Write(arr[i, j] + "\t");
    Console.WriteLine();
}
