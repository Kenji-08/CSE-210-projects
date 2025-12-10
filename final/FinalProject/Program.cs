using System;
using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using static InputHelper;

class Program
{
    static List<Race> _races = new List<Race>();
    static List<Driver> _drivers = new List<Driver>();
    static List<Car> _cars = new List<Car>();
    static List<Track> _tracks = new List<Track>();
    static List<Segment> _segments = new List<Segment>();
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
                    "\t3. Display Saves\n" +
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
        // LOAD ORDER IS VERY IMPORTANT
        LoadCars(_gameData);
        LoadDrivers(); // TODO: parse _gameData when it's configurable
        LoadSegments(_gameData);
        LoadTracks(_gameData);
        LoadRaces(_gameData);
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
    static void LoadDrivers() // TODO: add more player fields when applicable
    {
        Dictionary<string, string> playerSection = _currentSave.GetSection("player");
        Dictionary<string, string> npcSection = _gameData.GetSection("drivers");

        // Player loading
        _drivers.Add(new Driver(playerSection["name"], _cars[int.Parse(playerSection["carID"])]));

        // npc loading
        foreach (KeyValuePair<string, string> data in npcSection)
        {
            string id = data.Key;
            string raw = data.Value;
            string[] parts = raw.Split(',');

            // Format: DriverID=Name,CarID
            Driver d = new Driver(parts[0], _cars[int.Parse(parts[1]) - 1]);

            _drivers.Add(d);
        }
    }

    static void LoadTracks(IniFile ini)
    {
        Dictionary<string, string> section = ini.GetSection("tracks");

        foreach (KeyValuePair<string, string> data in section)
        {
            string id = data.Key;
            string[] parts = data.Value.Split(',');

            string name = parts[0];
            int laps = int.Parse(parts[1]);
            float length = float.Parse(parts[2]);
            int indexAmt = int.Parse(parts[3]);

            List<Segment> segmentsToAdd = new List<Segment>();
            for (int i = 4; i < 4+indexAmt; i++)
                segmentsToAdd.Add(_segments[int.Parse(parts[i])-1]);

            Track track = new Track(name, laps, length, segmentsToAdd);

            _tracks.Add(track);
        }
    }

    static void LoadSegments(IniFile ini)
    {
        Dictionary<string, string> section = ini.GetSection("segments");

        foreach (KeyValuePair<string, string> data in section)
        {
            Segment seg;
            string id = data.Key;
            string[] parts = data.Value.Split(',');

            string type = parts[0];          // straight / corner
            float length = float.Parse(parts[1]);
            int index = int.Parse(parts[2]);

            if (type == "corner")
            {
                float difficulty = float.Parse(parts[3].Replace("f", ""));
                float maxSpeed = float.Parse(parts[4]);
                seg = new CornerSegment(length, index, difficulty, maxSpeed);
            }
            else { seg = new StraightSegment(length, index); }

            _segments.Add(seg);
        }
    }

    static void LoadRaces(IniFile ini)
    {
        Dictionary<string, string> section = ini.GetSection("races");

        foreach (KeyValuePair<string, string> data in section)
        {
            string id = data.Key;
            string[] parts = data.Value.Split(',');

            string name = parts[0];
            int trackIndex = int.Parse(parts[1]) - 1;
            bool isFinished = bool.Parse(parts[11]);
            if (isFinished)
            {
                List<string> places = new List<string>();
                for (int i = 9 ; i < 17 ; i++)
                {
                    places.Add(_drivers[int.Parse(parts[i])-1].GetName());
                }
            }

            List<Driver> driverIDs = new List<Driver>();
            for (int i = 2; i < 10; i++)
                driverIDs.Add(_drivers[int.Parse(parts[i])-1]);

            string weather = parts[^1];
            
            // Format: RaceID=Name,trackID,drivers,weather,isFinished,places
            Race race = new Race(name, _tracks[trackIndex], driverIDs, weather, isFinished);

            _races.Add(race);
        }
    }
}