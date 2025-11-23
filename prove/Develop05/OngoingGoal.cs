using System.Text.Json.Serialization;

class OngoingGoal : Goal
{
    public OngoingGoal(){}
    public OngoingGoal(string title, string desc, int score) : base(title, desc, score){}

    public override bool isChecked()
    {
        return false;
    }
}