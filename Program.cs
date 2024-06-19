class Program
{
    public static int Main(string[] args)
    {
        var help = ArgumentParser.GetBoolean(args, "--help") || ArgumentParser.GetBoolean(args, "-h");
        if (help)
        {
            PrintHelp();

            return 0;
        }
        var inFile = ArgumentParser.GetString(args, "--in");
        var outFile = ArgumentParser.GetString(args, "--out");

        string[][] tableArray = ReadCSV(inFile);
        var md = FormatMD(tableArray);
        Console.WriteLine(md);

        return 0;
    }

    private static string FormatMD(string[][] tableArray)
    {
        // Проверка размера таблицы
        if (tableArray.Length == 0)
        {
            return "";
        }

        List<string> lines = new List<string>();

        // Получение размеров столбцов
        List<int> colSizes = new List<int>();
        for (int i = 0; i < tableArray[0].Length; i++)
        {
            var size = GetColumnSize(tableArray, i);
            colSizes.Add(size);
        }

        // Преобразование таблицы в текст MD
        for (int i = 0; i < tableArray.Length; i++)
        {
            var row = tableArray[i];
            string text = "|";
            for (int j = 0; j < row.Length; j++)
            {
                var columnSize = colSizes[j];
                text += " " + row[j].PadRight(columnSize, ' ') + " |";
            }
            lines.Add(text);

            // |---|---|...|---|
            if (i == 0)
            {
                text = "|";
                for (int j = 0; j < row.Length; j++)
                {
                    var columnSize = colSizes[j];
                    text += "".PadRight(columnSize + 2, '-') + "|";
                }
                lines.Add(text);
            }
        }

        // Склейка текста и вывод
        return string.Join("\n", lines);
    }

    private static string[][] ReadCSV(string inFile)
    {
        string[] csv = File.ReadAllLines(inFile);
        List<string[]> table = new List<string[]>();
        foreach (var row in csv)
        {
            string[] cols = row.Split(',');
            List<string> colsWithotWiteSpaces = new List<string>();
            foreach (var col in cols)
            {
                colsWithotWiteSpaces.Add(col.Trim());
            }
            table.Add(colsWithotWiteSpaces.ToArray());
        }

        string[][] tableArray = table.ToArray();
        return tableArray;
    }

    private static void PrintHelp()
    {
        Console.WriteLine("Usage: Program --in < inputfile > --out < outputfile > [-d,--debug][-h,--help]");
        Console.WriteLine("Options:");
        Console.WriteLine("  --in        Specifies the input CSV file");
        Console.WriteLine("  --out       Specifies the output file(not implemented yet)");
        Console.WriteLine("  -d,--debug  Enables debug mode for detailed error output");
        Console.WriteLine("  -h,--help   Displays this help message   ");
    }

    static int GetColumnSize(string[][] table, int index)
    {
        int size = 1;
        for (int i = 0; i < table.Count(); i++)
        {
            var value = table[i][index];
            if (size < value.Length)
            {
                size = value.Length;
            }
        }
        return size;
    }
}
