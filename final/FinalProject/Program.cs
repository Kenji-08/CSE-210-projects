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
    static string _currentSaveFileName;
    static string _defaultSavePath = "./Saves/";

    static void Main(string[] args)
    {
        _gameData = new IniFile($"./common/GameData.ini");
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
                        end = true;
                        NewGame();
                        break;
                    case 2:
                        end = true;
                        LoadGame();
                        break;
                    case 3:
                        DisplaySaves();
                        break;
                    case 4:
                        end = true;
                        Environment.Exit(0);
                        break;
                    default:
                        throw new Exception("Sorry that is not a valid option please try again.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


    }

    static void SaveGame() // TODO:
    {
        Driver player = _drivers[0];
        float[] skills = player.GetSkills();
        string podium = "";

        _currentSave.Set("player", "reaction", Convert.ToString(skills[0]));
        _currentSave.Set("player", "cornering", Convert.ToString(skills[1]));
        _currentSave.Set("player", "agility", Convert.ToString(skills[2]));


        foreach (Race race in _races) // Get places
        {
            podium = "";
            if (race.IsFinished())
            {
                foreach (string d in race.GetPodium())
                {
                    podium += $"{d},";
                }
                podium = podium.Remove(podium.Length - 1); // Gets rid of the leading comma
                _currentSave.Set("podiums", race.GetName(), podium);
                _currentSave.Set("places", race.GetName(), Convert.ToString(race.GetDriverPlace(player.GetName())));
            }
        }
        _currentSave.Save($"{_defaultSavePath}{_currentSaveFileName}");
    }

    static void LoadGame()
    {
        DisplaySaves(); // REMOVE: if not using this then get rid of it

        _currentSaveFileName = Input<string>("Please enter the file name to load: ");
        if (!_currentSaveFileName.Contains(".ini"))
        {
            _currentSaveFileName += ".ini";
        }

        // try
        // {
        _currentSave = new IniFile($"{_defaultSavePath}{_currentSaveFileName}");
        LoadCommonData();
        RunGame();
        // }
        // catch (Exception e) // CHANGE: if wanted
        // {
        //     Console.WriteLine("Sorry something went wrong returning to menu...");
        //     Console.WriteLine();
        //     Thread.Sleep(2000);
        //     StartMenu();
        // }
    }

    static void NewGame()
    {
        _currentSaveFileName = Input<string>("Please enter the name of this save file: ");
        if (!_currentSaveFileName.Contains(".ini"))
        {
            _currentSaveFileName += ".ini";
        }

        _currentSave = new IniFile($"./common/SaveTemp.ini");
        string name = Input<string>("What is your drivers name? ");

        LoadCommonData();
        Driver player = _drivers[0];

        Console.WriteLine("Car selection:");
        foreach (Car car in _cars)
        {
            Console.WriteLine(car.GetStats());
        }

        int carID = Input<int>("Please enter the ID number of the car you want to drive for this career: ");
        player.SetCar(_cars[carID - 1]);

        player.AllocateSkills();
        float[] skills = player.GetSkills();

        _drivers[0] = player; // Updates the player in the drivers list


        _currentSave.Set("player", "name", name);
        _currentSave.Set("player", "reaction", Convert.ToString(skills[0]));
        _currentSave.Set("player", "cornering", Convert.ToString(skills[1]));
        _currentSave.Set("player", "agility", Convert.ToString(skills[2]));
        _currentSave.Set("player", "carID", Convert.ToString(carID));
        _currentSave.Set("player", "currentRace", "0");

        _currentSave.Save($"{_defaultSavePath}{_currentSaveFileName}");
        RunGame();
    }

    static void LoadCommonData()
    {
        // LOAD ORDER IS VERY IMPORTANT
        LoadCars(_gameData);
        LoadDrivers();
        LoadSegments(_gameData);
        LoadTracks(_gameData);
        LoadRaces(_gameData);
    }

    static void RunGame()
    {
        foreach (Race race in _races)
        {
            if (!race.IsFinished()) // Runs only races in progress.
            {
                race.StartRace();
                if (race.GetName() != "Grand Prix")
                {
                    string option = Input<string>("Would you like to save & continue (s) continue without saving (c) or quit & save (q)? ");
                    if (option.ToLower() == "s") { SaveGame(); }
                    else if (option.ToLower() == "q") { SaveGame(); Environment.Exit(0); }

                    _drivers[0].AllocateSkills();
                }
                else
                {
                    SaveGame();
                    // TODO: add career stats maybe?
                }
            }
        }
    }

    static void DisplaySaves()
    {
        string root = "./Saves/";

        // Get a list of all subdirectories

        IEnumerable<string> files = from file in Directory.EnumerateFiles(root) select file;
        Console.WriteLine("Files: {0}", files.Count<string>().ToString());
        Console.WriteLine("List of Files");
        foreach (string file in files)
        {
            Console.WriteLine("{0}", file);
        }
    }

    static void LoadCars(IniFile ini)
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
        _drivers.Add(new Driver(playerSection["name"], Single.Parse(playerSection["reaction"]), Single.Parse(playerSection["cornering"]), Single.Parse(playerSection["agility"]), _cars[int.Parse(playerSection["carID"]) - 1]));

        // npc loading
        foreach (KeyValuePair<string, string> data in npcSection)
        {
            string id = data.Key;
            string raw = data.Value;
            string[] parts = raw.Split(',');

            // Format: DriverID=Name,Reaction,CornerSkill,Agility,CarID
            Driver d = new Driver(parts[0], float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3]), _cars[int.Parse(parts[4]) - 1]);

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
            for (int i = 4; i < 4 + indexAmt; i++)
                segmentsToAdd.Add(_segments[int.Parse(parts[i]) - 1]);

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
            int index = int.Parse(parts[2])-1;

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
                for (int i = 9; i < 17; i++)
                {
                    places.Add(_drivers[int.Parse(parts[i]) - 1].GetName());
                }
            }

            List<Driver> driverIDs = new List<Driver>();
            for (int i = 2; i < 10; i++)
                driverIDs.Add(_drivers[int.Parse(parts[i]) - 1]);

            string weather = parts[^1];

            // Format: RaceID=Name,trackID,drivers,weather,isFinished,places
            Race race = new Race(name, _tracks[trackIndex], driverIDs, weather, isFinished);

            _races.Add(race);
        }
    }
}