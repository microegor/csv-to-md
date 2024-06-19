class Program
{
    public static int Main(string[] args)
    {
        var help = ArgumentParser.GetBoolean(args, "--help") || ArgumentParser.GetBoolean(args, "-h");
        if (help)
        {
            Console.WriteLine("Usage: Program --in < inputfile > --out < outputfile > [-d,--debug][-h,--help]");
            Console.WriteLine("Options:");
            Console.WriteLine("  --in        Specifies the input CSV file");
            Console.WriteLine("  --out       Specifies the output file(not implemented yet)");
            Console.WriteLine("  -d,--debug  Enables debug mode for detailed error output");
            Console.WriteLine("  -h,--help   Displays this help message   ");
            return 0;
        }
        var inFile = ArgumentParser.GetString(args, "--in");
        Console.WriteLine($"--in: {inFile}");
        var outFile = ArgumentParser.GetString(args, "--out");
        Console.WriteLine($"--out: {outFile}");

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
        for (int i = 0; i < tableArray.Length; i++)
        {
            var row = tableArray[i];
            string text = "|";
            for (int j = 0; j < row.Length; j++)
            {
                var columnSize = GetColumnSize(tableArray, j);
                text += " " + row[j].PadRight(columnSize, ' ') + " |";
            }
            Console.WriteLine(text);

            // |---|---|...|---|
            if (i == 0)
            {
                var colSize = row.Length;
                text = "|";
                for (int j = 0; j < colSize; j++)
                {
                    var columnSize = GetColumnSize(tableArray, j);
                    text += "".PadRight(columnSize + 2, '-') + "|";
                }
                Console.WriteLine(text);
            }
        }

        return 0;
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
