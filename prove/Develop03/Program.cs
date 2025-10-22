using System;
using System.Drawing;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        Reference ref1 = new Reference("Mosiah", "15", "21");
        Scripture script1 = new Scripture(ref1, "And there cometh a resurrection, even a first resurrection; yea, even a resurrection of those that have been, and who are, and who shall be, even until the resurrection of Christ—for so shall he be called.");
        MenuLoop(script1);
    }

    static void MenuLoop(Scripture scripture)
    {
        string input = "";
        Console.Clear();
        scripture.DisplayRender();
        do
        {
            Console.WriteLine("\nPress ENTER to remove a random word, press r to reveal the whole verse, press q to quit");
            input = Console.ReadLine();
            if (input == "q")
            {
                break;
            }
            else if (input == "r")
            {
                Console.Clear();
                scripture.Display();
            }
            else
            {
                scripture.HideWords();
                Console.Clear();
                scripture.DisplayRender();

                if (scripture.IsAllHidden())
                {
                    Console.WriteLine("All words have been hidden");
                    break;
                }
            }

        } while (input != "q");
    }
}