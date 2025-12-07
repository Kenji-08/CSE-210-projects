using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;
using static InputHelper;

class Program
{
    static List<Race> _races;
    static List<Driver> _drivers;
    static List<Car> _cars;
    static IniFile gameData;

    static void Main(string[] args)
    {
        StartMenu();
        // LoadGame();
    }

    static void StartMenu()
    {
        bool end = false;

        while (!end)
        {
            try
            {
                Console.Clear();
                Console.WriteLine(
                    "Welcome to the Race Simulator\n" +
                    "\t1. New game\n" +
                    "\t2. Load Game"
                );
                int option = Input<int>("Please type an option: ");
                switch (option)
                {
                    case 1:
                        NewGame();
                        break;
                    case 2:
                        LoadGame();
                        break;
                    default:
                        throw new Exception("Sorry that is not a valid option please try again.");
                }

                // Went through successfully
                end = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


    }

    static void SaveGame() // TODO:
    {
        string filePath = "";


    }

    static void LoadGame()
    {
        DisplaySaves(); // REMOVE: if not using this then get rid of it
        string path = Input<string>("Please enter the file name to load: ");

        IniFile ini = new("GameData.ini");
    }

    static void NewGame() // TODO:
    {
        gameData = new IniFile("GameData.ini");
        
        // Read a car
        string car1 = gameData.Get("cars", "car1");
        Console.WriteLine($"Car1: {car1}");  // Output: Speedster,8,6,5

    }

    static void RunGame() // TODO:
    {

    }

    static void DisplaySaves() // TODO: maybe
    {

    }

    static void LoadCars() // TODO:
    {
        Dictionary<string, string> carSection = gameData.GetSection("cars");

        
    }
    static void LoadRaces(){}
    static void LoadTracks(){} // REMOVE: if needed
}