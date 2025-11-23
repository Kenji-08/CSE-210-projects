using System.Text.Json.Serialization;

class SimpleGoal : Goal
{
    public SimpleGoal(string title, string desc, int score) : base(title, desc, score){}

    [JsonConstructor]
    public SimpleGoal(){}

}