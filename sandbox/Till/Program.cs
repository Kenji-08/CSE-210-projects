namespace Till;

class Program
{
    static void Main(string[] args)
    {
        // Console.WriteLine("Hello, World!");
        Bin twentyBin = new Bin("Twenties", 20.00, 3);
        Bin tenBin = new Bin("Ten", 10.00, 10);
        Bin pennyBin = new Bin("Penny", 0.01, 50);

        pennyBin.Alter(11);
        Console.WriteLine(pennyBin.GetDenomination());
        Console.WriteLine(pennyBin.GetCount());
    }
}
