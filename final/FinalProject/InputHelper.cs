static class InputHelper
{
    // Wrote this because I find spamming readline and writeline annoying
    public static T Input<T>(string prompt)
    {
        Console.Write(prompt);
        string line = Console.ReadLine();
        try
        {
            Type targetType = typeof(T);
            if (targetType == typeof(int) && int.TryParse(line, out int iResult))
                return (T)(object)iResult;
            else if (targetType == typeof(float) && float.TryParse(line, out float dResult))
                return (T)(object)dResult;
            else { return (T)(object)line; }
        }
        catch
        {
            Console.WriteLine($"Error converting input to type {typeof(T).Name}.");
        }

        // Did not convert successfully 
        return (T)(object)"";
    }
}