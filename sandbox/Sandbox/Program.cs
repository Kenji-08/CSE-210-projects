using System;

class Program
{
    static void Main(string[] args)
    {
        // DateTime x = DateTime.Now;
        // Console.WriteLine(x.TimeOfDay);
        // x = x.AddSeconds(10);
        // Console.WriteLine(x.TimeOfDay);
        List<string> places = ["Fred", "Alice", "Jennifer", "Lawerence"];
        List<string> podium = places.GetRange(0, 3);
        string podiumString = "";
        foreach (string d in podium)
        {
            podiumString += $"{d},";
            Console.WriteLine(podiumString);
        }
        podiumString = podiumString.Remove(podiumString.Length-1);
        Console.WriteLine(podiumString);
    }
}