using System.Text.Json.Serialization;

class NegativeGoal : Goal
{
    public NegativeGoal(string title, string desc, int score) : base(title, desc, score){}

    [JsonConstructor]
    public NegativeGoal(){}

    public override void Award()
    {
        TotalScore -= Score;
        Status = true;
        Console.WriteLine($"Oh no! You have lost {Score} points!");
    }

}