using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;
using static InputHelper;

class Program
{
    static List<Race> _races = new List<Race>();
    static List<Driver> _drivers = new List<Driver>();
    static List<Car> _cars = new List<Car>();
    static IniFile _gameData;
    static IniFile _currentSave;
    static string _defaultSavePath = "./Saves/";

    static void Main(string[] args)
    {
        _gameData = new IniFile($"{_defaultSavePath}GameData.ini");
        StartMenu();
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
                    "\t3. Display Saves\n"+
                    "\t4. Quit"
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
                        DisplaySaves();
                        break;
                    case 4:
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

        // try
        // {
        _currentSave = new IniFile($"{_defaultSavePath}{fileName}");
        LoadCommonData();
        // }
        // catch (Exception e) // CHANGE: if wanted
        // {
        //     Console.WriteLine("Sorry something went wrong returning to menu...");
        //     Console.WriteLine();
        //     Thread.Sleep(2000);
        //     StartMenu();
        // }
    }

    static void NewGame() // TODO: add loading in common stuff and make skill allocation name and such
    {
        string fileName = Input<string>("Please enter the name of this save file: ");
        _currentSave = new IniFile($"{_defaultSavePath}{fileName}");
    }

    static void LoadCommonData()
    {
        LoadCars(_gameData);
        LoadDrivers(); // TODO: parse _gameData when it's configurable
        LoadRaces(); // TODO: parse _gameData when it's configurable
        RunGame();
    }

    static void RunGame() // TODO:
    {
        foreach (Race race in _races)
        {
            race.StartRace();
        }
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
            string[] parts = raw.Split(',');

            // Format: CarID=Name,Speed,TopSpeed,Acceleration,Brakes,TireCompound
            Car c = new Car(id, parts[0], float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3]), float.Parse(parts[4]), new Tire(parts[5]));

            _cars.Add(c);
        }
    }
    static void LoadDrivers() // TODO: Make configurable
    {
        Driver __player = new Driver("Player", _cars[0]);
        Driver __driver1 = new Driver("Eduardo", _cars[1]);

        _drivers = [__player, __driver1];
    }
    static void LoadRaces() // TODO: Make configurable
    {
        Segment straight1 = new StraightSegment(250, 0);
        Segment turn1 = new CornerSegment(250, 1, 0.8f, 15);
        Segment straight2 = new StraightSegment(250, 2);
        Segment turn2 = new CornerSegment(250, 3, 0.8f, 15);

        List<Segment> segments = [straight1, turn1, straight2, turn2];

        Track track = new Track("Circuit", segments, 1000);
        Race race = new Race("Test Track", track, _drivers);
        _races.Add(race);
    }
    static void LoadTracks() { } // REMOVE: if needed
}