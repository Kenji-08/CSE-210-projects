namespace Anki;

class Program
{
    static void Main(string[] args)
    {
        MenuLoop();
    }

    static void MenuLoop()
    {
        Flashcard card = new Flashcard();
        string input = "";
        Console.WriteLine("""
                            Anki Core 2000 CSV Writer.
                            Press w to write cards, s to save to the CSV file
                        """);
    }

    static void SaveCardFile(Flashcard card)
    {
        string fileName = "Anki.csv";
        using (StreamWriter sw = File.AppendText(fileName))
        {
            sw.WriteLine($"");
        }
    }
}
