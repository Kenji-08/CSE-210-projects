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

    static void Main(string[] args)
    {

        LoadGame();
    }

    static void StartMenu()
    {

    }

    static void SaveGame() // TODO:
    {
        string filePath = "";

        StringBuilder csv = new StringBuilder();

        using (StreamReader reader = new StreamReader(filePath))
        {
            while (!reader.EndOfStream)
            {
            }
        }

        File.WriteAllText(filePath, csv.ToString());
    }

    static void LoadGame()
    {
        DisplaySaves(); // REMOVE: if not using this then get rid of it
        // string folderPath = Input<string>("Type the name of the save you would like to load: ");
        string folderPath = "bin/Debug/net8.0/Saves/Save0";

        List<List<String>> raceProgresses = new List<List<string>>();
        List<Segment> segmentsToAdd = new List<Segment>();
        List<Track> tracksToAdd = new List<Track>();

        using (StreamReader reader = new StreamReader($"{folderPath}/races.csv"))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] v = line.Split(';');

                // ID, isCompleted, name, length, weather
                raceProgresses.Add(new List<string> { v[0], v[1], v[2], v[3], v[4] });
            }
        }

        // Contains all the segments inside of the track
        using (StreamReader reader = new StreamReader($"{folderPath}/tracks.csv"))
        {
            int SegmentIDToAdd = 0;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] v = line.Split(';');

                int trackID = Convert.ToInt32(v[0]);
                int segmentIndex = Convert.ToInt32(v[1]);
                string segmentType = v[2];
                float length = Convert.ToSingle(v[3]);

                if (trackID > SegmentIDToAdd)
                {
                    tracksToAdd.Add(new Track(segmentType, segmentsToAdd, length));
                    segmentsToAdd.Clear();
                    SegmentIDToAdd = trackID;
                }

                if (segmentType == "straight")
                {
                    segmentsToAdd.Add(new StraightSegment(length, segmentIndex));
                }
                else
                {
                    segmentsToAdd.Add(new CornerSegment(length, segmentIndex, Convert.ToSingle(v[4])));
                }
            }
        }

        using (StreamReader reader = new StreamReader($"{folderPath}/cars.csv"))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] v = line.Split(';');
                int topSpeed = Convert.ToInt32(v[0]);
                float speed = Convert.ToSingle(v[1]);
                float acceleration = Convert.ToSingle(v[2]);
                string tire = v[3];

                // BaseSpeed, Acceleration, tire
                _cars.Add(new Car(speed, acceleration, topSpeed, new Tire(tire)));
            }
        }

        using (StreamReader reader = new StreamReader($"{folderPath}/drivers.csv"))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] v = line.Split(';');
                string name = v[0];
                float reaction = Convert.ToSingle(v[1]);
                float cornerSkill = Convert.ToSingle(v[2]);
                float agility = Convert.ToSingle(v[3]);
                int wins = Convert.ToInt32(v[4]);
                int carID = Convert.ToInt32(v[5]);

                // Name, Reaction, CornerSkill, Agility, Wins, CarID, CurrentSkillPoints
                _drivers.Add(new Driver(name, reaction, cornerSkill, agility, _cars[carID]));
            }
        }


        for (int race = 0; race < raceProgresses.Count; race++)
        {
            int index = Convert.ToInt32(raceProgresses[0]);
            bool isCompleted = Convert.ToBoolean(raceProgresses[1]);
            string name = Convert.ToString(raceProgresses[2]);
            int length = Convert.ToInt32(raceProgresses[3]);
            string weather = Convert.ToString(raceProgresses[4]);

            _races.Add(new Race(name, tracksToAdd[race], length, _drivers));
        }
    }

    static void NewGame() // TODO:
    {

    }

    static void RunGame() // TODO:
    {

    }

    static void DisplaySaves() // TODO: maybe
    {

    }
}