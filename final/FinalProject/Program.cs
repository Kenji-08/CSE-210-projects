using System;
using System.Collections;
using System.ComponentModel;
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
    static string _currentSaveName;
    static string _defaultSavePath = "./Saves/";
    static int _defaultTickRate = 250;
    static int _tickSpeed = 1;
    static int _displaySpeed = 1;
    static int _defaultDisplayRate = 1000;

    static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Epilepsy Warning! This game contains flashing text especially at higher display speeds! ");
        Input<string>("Press enter if you understand the risks of a seizure if you have a sensitivity to flashing lights ");
        Console.Clear();
        StartMenu();
    }

    static void StartMenu()
    {
        Console.Clear();
        while (true)
        {
            Console.WriteLine(
                "Welcome to the Race Simulator\n" +
                "\t1. New game\n" +
                "\t2. Load Game\n" +
                "\t3. Display Saves\n" +
                "\t4. Simulation Settings\n" +
                "\t5. Quit"
            );

            int option = Input<int>("Option: ");

            switch (option)
            {
                case 1: NewGame(); return;
                case 2: LoadGame(); return;
                case 3: DisplaySaves(); break;
                case 4: SettingsMenu(); break;
                case 5: Environment.Exit(0); break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void SaveGame()
    {
        string saveFolder = $"{_defaultSavePath}{_currentSaveName}/";

        Driver player = _drivers[0];
        float[] skills = player.GetSkills();

        _currentSave.Set("player", "reaction", Convert.ToString(skills[0]));
        _currentSave.Set("player", "cornering", Convert.ToString(skills[1]));
        _currentSave.Set("player", "agility", Convert.ToString(skills[2]));


        foreach (Race race in _races) // Get places
        {
            if (race.IsFinished())
            {
                string podium = string.Join(",", race.GetPodium());
                _currentSave.Set("podiums", race.GetName(), podium);
                _currentSave.Set("places", race.GetName(), Convert.ToString(race.GetDriverPlace(player.GetName())));
            }
        }

        _currentSave.Save($"{saveFolder}{_currentSaveName}.ini");

        // GameData saving
        for (int r = 0; r < _races.Count(); r++)
        {
            if (_races[r].IsFinished())
            {
                string raceValue = _gameData.Get("races", $"race{r + 1}");
                List<string> valueList = raceValue.Split(",").ToList();
                valueList[11] = "true"; // Updates completion
                if (valueList.Count <= 12)
                {
                    valueList.AddRange(_races[r].GetPlaces());
                }

                string updatedValues = string.Join(",", valueList);

                _gameData.Set("races", $"race{r + 1}", updatedValues);
            }
        }
        _gameData.Save($"{saveFolder}GameData.ini");
    }

    static void LoadGame()
    {
        DisplaySaves();

        _currentSaveName = Input<string>("Enter save folder name: ");
        string folder = $"{_defaultSavePath}{_currentSaveName}/";

        if (!Directory.Exists(folder))
        {
            Console.Clear();
            Console.WriteLine("Save folder not found.");
            StartMenu();
            return;
        }

        // Load the save’s own GameData.ini
        _gameData = new IniFile($"{folder}GameData.ini");

        // Load the save’s player file
        _currentSave = new IniFile($"{folder}{_currentSaveName}.ini");

        LoadCommonData();
        RunGame();
    }


    static void NewGame()
    {
        _currentSaveName = Input<string>("Enter new save name: ");
        string folder = $"{_defaultSavePath}{_currentSaveName}/";

        Directory.CreateDirectory(folder);

        // Copy the common GameData template to the new save folder
        File.Copy("./common/GameData.ini", $"{folder}GameData.ini", overwrite: true);

        // Create the base player save file
        File.Copy("./common/SaveTemp.ini", $"{folder}{_currentSaveName}.ini", overwrite: true);

        // Load both
        _gameData = new IniFile($"{folder}GameData.ini");
        _currentSave = new IniFile($"{folder}{_currentSaveName}.ini");

        // Driver creation
        string name = Input<string>("Driver name: ");

        LoadCommonData();
        Driver player = _drivers[0];

        Console.WriteLine("Car selection:");
        foreach (Car c in _cars) Console.WriteLine(c.GetStats());

        int carID = Input<int>("Car ID: ");
        player.SetCar(_cars[carID - 1]);
        player.AllocateSkills();

        // Save initial data
        float[] s = player.GetSkills();
        _currentSave.Set("player", "name", name);
        _currentSave.Set("player", "reaction", s[0].ToString());
        _currentSave.Set("player", "cornering", s[1].ToString());
        _currentSave.Set("player", "agility", s[2].ToString());
        _currentSave.Set("player", "carID", carID.ToString());
        _currentSave.Set("player", "currentRace", "0");

        _currentSave.Save($"{folder}{_currentSaveName}.ini");

        RunGame();
    }

    static void DisplaySaves()
    {
        if (!Directory.Exists(_defaultSavePath))
        {
            Console.WriteLine("No save directory found.");
            return;
        }

        string[] saves = Directory.GetDirectories(_defaultSavePath);

        Console.WriteLine("Available Saves:");
        foreach (string s in saves)
        {
            Console.WriteLine(Path.GetFileName(s)); // Folder name only
        }
    }

    static void LoadCommonData()
    {
        _cars.Clear();
        _drivers.Clear();
        _tracks.Clear();
        _segments.Clear();
        _races.Clear();

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
            if (!race.IsFinished())
            {
                race.StartRace(_defaultTickRate/_tickSpeed, _defaultDisplayRate/_displaySpeed);

                if (race.GetName() != "Grand Prix")
                {
                    _drivers[0].AllocateSkills();

                    string option = Input<string>("Save & Continue (s), Continue without saving (c), Quit (q): ").ToLower();

                    if (option == "s") SaveGame();
                    else if (option == "q") { SaveGame(); Environment.Exit(0); }
                }
                else
                {
                    SaveGame();
                }
            }
        }
        DisplayCareerStats();
        Input<string>("Press enter to return to the menu");
        StartMenu();
    }

    static public void DisplayCareerStats()
    {
        Console.Clear();
        foreach (Race r in _races)
        {
            Console.WriteLine(r.GetName());
            List<string> podium = r.GetPodium();
            for (int d = 0 ; d < 3 ; d++)
            {
                string placeString = "";
                switch (d)
                {
                    case 0: placeString="1st"; break;
                    case 1: placeString="2nd"; break;
                    case 2: placeString="3rd"; break;
                }
                Console.WriteLine($"\t{placeString} {podium[d]}");
            }
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
            Car c = new Car(
                id,
                parts[0],
                float.Parse(parts[1]),
                float.Parse(parts[2]),
                float.Parse(parts[3]),
                float.Parse(parts[4]),
                new Tire(parts[5])
            );

            _cars.Add(c);
        }
    }
    static void LoadDrivers()
    {
        Dictionary<string, string> playerSection = _currentSave.GetSection("player");
        Dictionary<string, string> npcSection = _gameData.GetSection("drivers");

        // Player loading
        _drivers.Add(new Driver(
            playerSection["name"],
            Single.Parse(playerSection["reaction"]),
            Single.Parse(playerSection["cornering"]),
            Single.Parse(playerSection["agility"]),
            _cars[int.Parse(playerSection["carID"]) - 1])
        );

        // npc loading
        foreach (KeyValuePair<string, string> data in npcSection)
        {
            string raw = data.Value;
            string[] parts = raw.Split(',');

            // Format: DriverID=Name,Reaction,CornerSkill,Agility,CarID
            Driver d = new Driver(
                parts[0],
                float.Parse(parts[1]),
                float.Parse(parts[2]),
                float.Parse(parts[3]),
                _cars[int.Parse(parts[4]) - 1]
            );

            _drivers.Add(d);
        }
    }

    static void LoadSegments(IniFile ini)
    {
        Dictionary<string, string> section = ini.GetSection("segments");

        foreach (KeyValuePair<string, string> data in section)
        {
            Segment seg;
            string[] parts = data.Value.Split(',');

            string type = parts[0]; // straight / corner
            float length = float.Parse(parts[1]);
            int index = int.Parse(parts[2]) - 1;

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

    static void LoadTracks(IniFile ini)
    {
        Dictionary<string, string> section = ini.GetSection("tracks");

        foreach (KeyValuePair<string, string> data in section)
        {
            string[] parts = data.Value.Split(',');

            string name = parts[0];
            int laps = int.Parse(parts[1]);
            float length = float.Parse(parts[2]);
            int indexAmt = int.Parse(parts[3]);

            List<Segment> segs = new List<Segment>();
            for (int i = 4; i < 4 + indexAmt; i++)
                segs.Add(_segments[int.Parse(parts[i]) - 1]);

            Track track = new Track(name, laps, length, segs);

            _tracks.Add(track);
        }
    }

    static void LoadRaces(IniFile ini)
    {
        Dictionary<string, string> section = ini.GetSection("races");

        foreach (KeyValuePair<string, string> data in section)
        {
            string[] parts = data.Value.Split(',');

            string name = parts[0];
            int trackIndex = int.Parse(parts[1]) - 1;
            bool isFinished = bool.Parse(parts[11]);

            List<Driver> driverIDs = new List<Driver>();
            for (int i = 2; i < 10; i++)
                driverIDs.Add(_drivers[int.Parse(parts[i]) - 1]);

            string weather = parts[10];

            List<string> places = new List<string>();

            if (isFinished && parts.Length >= 12)
            {
                for (int i = 12; i < parts.Length; i++)
                {
                    places.Add(parts[i]);
                }
            }

            Race race = new Race(name, _tracks[trackIndex], driverIDs, weather, isFinished, places);
            _races.Add(race);
        }
    }

    static void SettingsMenu()
    {
        while (true)
        {
            Console.WriteLine(
                "Settings\n" +
                "\t1. Simulation speed\n" +
                "\t2. Display speed (Affects text refresh speed seizure warning if you change this!) \n" +
                "\t3. Back\n"
            );
            int option = Input<int>("Option: ");

            switch (option)
            {
                case 1:
                    _tickSpeed = Input<int>("Type the new simulation speed multiplier (Don't use above 2 if you have epilepsy): ");
                    if (_tickSpeed > 2)
                    {
                        _displaySpeed = _tickSpeed * 4;
                    }
                    break;
                case 2:
                    _displaySpeed = Input<int>("Type the new display speed multiplier (type in 4 times the sim speed to match it): ");
                    break;
                case 3:
                    return;
            }
        }
    }
}