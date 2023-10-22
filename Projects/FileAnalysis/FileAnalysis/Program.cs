class ExtensionData
{
    public int Count { get; set; }
    public long Size { get; set; }
}

class Program
{
    static void Main()
    {
        var dir = new DirectoryInfo(@"C:\");
        var extensions = new Dictionary<string, ExtensionData>();

        try
        {
            var files = GetFiles(dir, "*");

            foreach (var file in files)
            {
                var ext = file.Extension;
                if (string.IsNullOrEmpty(ext))
                    ext = "Без расширения";

                if (extensions.ContainsKey(ext))
                {
                    ExtensionData extData = extensions[ext];
                    extData.Count++;
                    extData.Size += file.Length;
                }
                else
                {
                    extensions[ext] = new ExtensionData { Count = 1, Size = file.Length };
                }
            }

            var totalFiles = files.Count;
            var totalSize = files.Sum(file => file.Length);

            var sortedExtensions = extensions.OrderByDescending(ext => ext.Value.Count).ToList();

            Console.WriteLine("+----------+----------------+------------+-----------------+---------------------------+----------------------+");
            Console.WriteLine(
                "| {0, -8} | {1, -14} | {2, -10} | {3, -15} | {4, -25} | {5, -20} |",
                  "№", "Расширение", "Кол-во", "Общий объём в Б",
                  "% от общего количества", "% от общего объёма");
            Console.WriteLine("+----------+----------------+------------+-----------------+---------------------------+----------------------+");

            int num = 1;
            foreach (var ext in sortedExtensions)
            {
                var extData = ext.Value;
                Console.WriteLine(
                    "| {0, -8} | {1, -14} | {2, -10} | {3, -15} | {4, -25} | {5, -20} |",
                      num++, ext.Key, extData.Count, extData.Size,
                      $"{100.0 * extData.Count / totalFiles:F2}%",
                      $"{100.0 * extData.Size / totalSize:F2}%");
            }
            Console.WriteLine("+----------+----------------+------------+-----------------+---------------------------+----------------------+");
            Console.WriteLine(
                "| {0, -25} | {1, -10} | {2, -15} | {3, -25} | {4, -20} |",
                  "TOTAL:", $"{totalFiles}", $"{totalSize}",
                  "100", "100");
            Console.WriteLine("+----------+----------------+------------+-----------------+---------------------------+----------------------+");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Нет доступа к файлам или каталогам.");
        }
        Console.ReadLine();
    }

    static List<FileInfo> GetFiles(DirectoryInfo dir, string searchPattern)
    {
        var files = new List<FileInfo>();
        foreach (var file in dir.GetFiles(searchPattern))
            files.Add(file);

        foreach (var subDir in dir.GetDirectories())
        {
            if (subDir.Name == "Documents" || subDir.Name == "Settings")
                continue; // с ними не работает
            try
            {
                files.AddRange(GetFiles(subDir, searchPattern));
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Нет доступа к {subDir.Name}");
            }
        }
        return files;
    }
}
