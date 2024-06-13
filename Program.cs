
class Program
{
    public static int Main(string[] args)
    {
        var inFile = ArgumentParser.GetString(args, "--in");
        Console.WriteLine($"--in: {inFile}");
        var outFile = ArgumentParser.GetString(args, "--out");
        Console.WriteLine($"--out: {outFile}");

        var csv = File.ReadAllLines(inFile);

        for (int i = 0; i < csv.Length; i++)
        {
            var item = csv[i];
            item = item.Replace(",", "|");
            Console.WriteLine($"|{item}|");

            if (i == 0)
            {
                var colSize = item.Split("|").Count();
                for (int j = 0; j < colSize; j++)
                {
                    Console.Write("|---");
                }
                Console.WriteLine("|");
            }
        }


        return 0;
    }
}
