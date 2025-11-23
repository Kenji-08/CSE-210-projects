// For showing creativity I added a negative goal and value goal. I also added a function called Input which is similar to pythons
// Input() but this one only handels int and string


using System;
using System.Drawing;
using System.Net.Quic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;
class Program
{
    private static List<Goal> _goals = new List<Goal>();
    private static int _totalPoints = 0;

    static void Main(string[] args)
    {
        int input = 0;
        Console.Clear();

        while (true)
        {
            Console.WriteLine($"\nYou have {_totalPoints} points.\n");
            Console.WriteLine("Menu Options:");
            Console.WriteLine("\t1. Create New Goal\n\t2. List Goals\n\t3. Save Goals");
            Console.WriteLine("\t4. Load Goals\n\t5. Record Event\n\t6. Quit");
            input = Input<int>("Select a choice from the menu: ");

            switch (input){
                case 1:
                    AddNewGoal();
                    break;
                case 2:
                    ListGoalsAndScores();
                    break;
                case 3:
                    SaveGoals();
                    break;
                case 4:
                    LoadGoals();
                    break;
                case 5:
                    RecordEvent();
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Error not a valid option!");
                    break;
            } 
        }
    }
    static void AddNewGoal()
    {
        Console.WriteLine("The types of goals are: ");
        Console.WriteLine("1. Simple Goal\n2. Ongoing Goal\n3. Checklist Goal\n4. Negative Goal\n5. Value Goal");
        Console.Write("Which type of goal would you like to create? ");
        
        // User input to be used for choosing goal type
        int goal = Convert.ToInt32(Console.ReadLine());

        string title = Input<String>("What is the name of your goal? ");
        string desc = Input<String>("What is a short description of it? ");
        Console.Write("What is the amount of points associated with this goal? ");
        int points = Convert.ToInt32(Console.ReadLine());

        switch (goal)
        {
            case 1:
                SimpleGoal simple = new SimpleGoal(title, desc, points);
                _goals.Add(simple);
                break;
            case 2:
                OngoingGoal ongoing = new OngoingGoal(title, desc, points);
                _goals.Add(ongoing);
                break;
            case 3:
                int max = Input<int>("How many times does this goal need to be accomplished for a bonus? ");
                int bonus = Input<int>("What is the bonus for accomplishing it that many times? ");
                ChecklistGoal checklist = new ChecklistGoal(title, desc, points, bonus, max);
                _goals.Add(checklist);
                break;
            case 4:
                // Ensures points is not negative
                if (points.ToString().Contains("-"))
                {
                    points = points* -1;
                }
                NegativeGoal negative = new NegativeGoal(title, desc, points);
                _goals.Add(negative);
                break;
            case 5:
                int target = Input<int>("What is the target number for this goal? ");
                ValueGoal value = new ValueGoal(title, desc, points, target);
                _goals.Add(value);
                break;
        }
    }
    static void ListGoalsAndScores()
    {
        Console.WriteLine("The goals are:");
        for (int g = 0 ; g < _goals.Count() ; g++)
        {
            Console.WriteLine($"{g+1}. {_goals[g].GetDisplayString()}");
        }
    }
    static void RecordEvent()
    {
        Console.WriteLine("The goals are:");
        for (int g = 0 ; g < _goals.Count() ; g++)
        {
            Console.WriteLine($"{g+1}.{_goals[g].GetDisplayString().Substring(3)}");
        }
        int goal = Input<int>("Which goal did you accomplish? ");
        _goals[goal-1].Award();
        
        _totalPoints = 0;
        foreach (Goal g in _goals)
        {
            _totalPoints += g.GetTotalScore();
        }
    }
    static void SaveGoals()
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        string jsonFile = JsonSerializer.Serialize(_goals, options);

        Console.WriteLine("Please type the file name to save as (don't include file extension): ");
        string fileName = Console.ReadLine() + ".json";
        File.WriteAllText(fileName, jsonFile);
    }
    static void LoadGoals()
    {
        string jsonPath = Input<string>("Please type name of the file to load (make sure it's a json file): ");
        string jsonFile;

        // Check if path has json extension
        if (jsonPath.Contains(".json")) {jsonFile = File.ReadAllText(jsonPath);}
        else {jsonFile = File.ReadAllText(jsonPath+".json");}    

        _goals = JsonSerializer.Deserialize<List<Goal>>(jsonFile);
        _totalPoints = 0;
        foreach (Goal goal in _goals)
        {
            _totalPoints += goal.GetTotalScore();
        }
    }

    // Wrote this because I find spamming readline and writeline annoying
    static T Input<T>(string prompt)
    {
        Console.Write(prompt);
        string line = Console.ReadLine();
        try
        {
            Type targetType = typeof(T);
            if (targetType == typeof(int) && int.TryParse(line, out int iResult))
                    return (T)(object)iResult;
            else{ return (T)(object)line; }
        }
        catch
        {
            Console.WriteLine($"Error converting input to type {typeof(T).Name}.");
        }

        // Did not convert successfully 
        return (T)(object)"";
    }

}