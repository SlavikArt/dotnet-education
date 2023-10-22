using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string text = File.ReadAllText("kobzar.txt");

        text = text.Replace("\r", "");
        text = text.Replace("\n", "");

        string textWithoutPunctuation = Regex.Replace(text, @"[\p{P}-[.]]+", "");

        string[] words = textWithoutPunctuation.Split(' ');

        Dictionary<string, int> wordFrequency = new Dictionary<string, int>();

        foreach (string word in words)
        {
            if (word.Length >= 3 && word.Length <= 20)
            {
                string lowcaseWord = word.ToLower();

                if (wordFrequency.ContainsKey(lowcaseWord))
                    wordFrequency[lowcaseWord]++;
                else
                    wordFrequency[lowcaseWord] = 1;
            }
        }

        List<KeyValuePair<string, int>> wordList = new List<KeyValuePair<string, int>>(wordFrequency);

        wordList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
        wordList.Reverse();

        Console.WriteLine("+-----+----------+-----------------+");
        Console.WriteLine("|  №  | Слово    | Встречается раз |");
        Console.WriteLine("+-----+----------+-----------------+");

        int num = 1;
        foreach (KeyValuePair<string, int> pair in wordList.Take(50))
        {
            Console.WriteLine("| {0} | {1} | {2} |", 
                num.ToString().PadRight(3),
                pair.Key.PadRight(8),
                pair.Value.ToString().PadRight(15));
            num++;
        }
        Console.WriteLine("+-----+----------+-----------------+");
    }
}
