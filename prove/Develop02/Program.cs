using System;
using System.IO;

class Program

{
    static Journal _currentJournal = new Journal();

    static void Main(string[] args)
    {
        // Console.WriteLine("Hello Develop02 World!");
        MenuLoop();
    }

    static void MenuLoop()
    {
        string x = "";
        do
        {
            Console.WriteLine("Write an entry (w), Save (s) Load (l) or Display (d) a journal? (q) will quit the program. ");
            x = Console.ReadLine();
            switch (x.ToLower())
            {
                case "w":
                    WriteNewEntry();
                    break;
                case "s":
                    SaveJournalFile();
                    break;
                case "l":
                    LoadJournalFile();
                    break;
                case "d":
                    _currentJournal.Display();
                    break;
                default:
                    break;
            }
        }
        while (x != "q");
    }

    static void WriteNewEntry()
    {
        Entry newEntry = new Entry();
        Prompt prompt = new Prompt();

        // Prompt & Response
        newEntry._givenPrompt = prompt.GeneratePrompt();
        Console.WriteLine(newEntry._givenPrompt);
        Console.WriteLine("Response:\n");
        newEntry._entryText = Console.ReadLine();

        // Date
        Console.WriteLine("Date:\n");
        newEntry._date = Console.ReadLine();

        // Time
        Console.WriteLine("Time (e.g. 10AM 3:15PM, Morning, Afternoon, etc.):\n");
        newEntry._time = Console.ReadLine();

        // Mood
        Console.WriteLine("Mood:\n");
        newEntry._mood = Console.ReadLine();

        _currentJournal.AppendEntry(newEntry);
    }

    static void SaveJournalFile()
    {
        Console.WriteLine("Name of file to save to: ");
        string fileName = Console.ReadLine();
        fileName += ".csv";
        foreach (Entry x in _currentJournal._entryList)
        {
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine($"{x._date} | {x._time} | {x._givenPrompt} | {x._mood} | {x._entryText} |");
            }
        }

    }

    static void LoadJournalFile()
    {
        Console.WriteLine("Enter file name to load: ");
        string fileName = Console.ReadLine();
        fileName += ".csv";

        Entry newEntry = new Entry();
        _currentJournal._entryList = new List<Entry>();

        using (StreamReader sr = File.OpenText(fileName))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] values = line.Split("|");
                newEntry._date = values[0];
                newEntry._time = values[1];
                newEntry._givenPrompt = values[2];
                newEntry._mood = values[3];
                newEntry._entryText = values[4];
                _currentJournal.AppendEntry(newEntry);
            }
        }
    }
}