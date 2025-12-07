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
    static IniFile _gameData;
    static IniFile _currentSave;
    static string _defaultSavePath = "./Saves/";

    static void Main(string[] args)
    {
        _gameData = new IniFile($"{_defaultSavePath}GameData.ini");
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
                // Console.Clear();
                Console.WriteLine(
                    "Welcome to the Race Simulator\n" +
                    "\t1. New game\n" +
                    "\t2. Load Game\n" +
                    "\t3. Quit"
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
                    case 3:
                        Environment.Exit(0);
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

        string fileName = Input<string>("Please enter the file name to load: ");
        if (!fileName.Contains(".ini"))
        {
            fileName += ".ini";
        }

        try
        {
            _currentSave = new IniFile($"{_defaultSavePath}{fileName}");
            LoadCars(_gameData);
            LoadDrivers(); // TODO: parse _gameData when it's configurable
            LoadRaces(); // TODO: parse _gameData when it's configurable
            RunGame();
        }
        catch (Exception e) // CHANGE: if wanted
        {
            Console.WriteLine("Sorry something went wrong returning to menu...");
            Thread.Sleep(2000);
            StartMenu();
        }
    }

    static void NewGame() // TODO:
    {
        string fileName = Input<string>("Please enter the name of this save file: ");
        _currentSave = new IniFile($"{_defaultSavePath}{fileName}");
    }

    static void RunGame() // TODO:
    {

    }

    static void DisplaySaves() // TODO: maybe
    {

    }

    static void LoadCars(IniFile ini) // TODO: finish constructor
    {
        Dictionary<string, string> section = _gameData.GetSection("cars");

        foreach (KeyValuePair<string, string> data in section)
        {
            string id = data.Key;
            string raw = data.Value;
            string[] parts = raw.Split();

            // Format: CarID=Name,Speed,TopSpeed,Acceleration,TireCompound
            Car c = new Car(int.Parse(id), parts[0], float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3]), new Tire(parts[4]));

            _cars.Add(c);
        }
    }
    static void LoadDrivers() // TODO: Make configurable
    {
        Driver __driver1 = new Driver("Eduardo", _cars[0]);

        _drivers = [__driver1];
    }
    static void LoadRaces() // TODO: Make configurable
    {
        Segment straight1 = new StraightSegment(250, 0);
        Segment turn1 = new CornerSegment(250, 1, 0.1f);
        Segment straight2 = new StraightSegment(250, 2);
        Segment turn2 = new CornerSegment(250, 3, 0.1f);

        List<Segment> segments = [straight1, turn1, straight2, turn2];

        Track track = new Track("Circuit", segments, 1000);
        Race race = new Race("Test Track", track, _drivers);
        _races.Add(race);
    }
    static void LoadTracks() { } // REMOVE: if needed
}