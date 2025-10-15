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
        Console.WriteLine("Anki CSV Writer. Press \'q\' to quit");
        do
        {
            // Kanji
            Console.WriteLine("Kanji: ");
            input = Console.ReadLine();
            card._kanji = input;

            // Furigana
            Console.WriteLine("Furigana: ");
            input = Console.ReadLine();
            card._furigana = input;
        } while (input != "q");
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
