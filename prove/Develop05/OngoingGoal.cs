using System.Text.Json.Serialization;

class OngoingGoal : Goal
{
    public OngoingGoal(string title, string desc, int score) : base(title, desc, score){}

    [JsonConstructor]
    public OngoingGoal(){}

    public override bool isChecked()
    {
        return false;
    }
}