class ArgumentParser
{
    public static bool GetBoolean(string[] args, string name)
    {
        for (int i = 0; i < args.Length; i++)
        {
            var item = args[i];
            if (item == name)
            {
                return true;
            }
        }
        return false;
    }

    public static string GetString(string[] args, string name)
    {
        for (int i = 0; i < args.Length; i++)
        {
            var item = args[i];
            if (item == name)
            {
                return args[i + 1];
            }
        }
        throw new Exception($"Argument '{name}' not found");
    }

    public static int GetInt32(string[] args, string name)
    {
        var value = GetString(args, name);
        return Convert.ToInt32(value);
    }
}
