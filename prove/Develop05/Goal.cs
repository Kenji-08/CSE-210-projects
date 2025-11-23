using System.Reflection;
using System.Text.Json.Serialization;

// If a new class is added make sure it's added here too.
[JsonPolymorphic(TypeDiscriminatorPropertyName = "Type")]
[JsonDerivedType(typeof(SimpleGoal), typeDiscriminator: "SimpleGoal")]
[JsonDerivedType(typeof(ChecklistGoal), typeDiscriminator: "ChecklistGoal")]
[JsonDerivedType(typeof(OngoingGoal), typeDiscriminator: "OngoingGoal")]
[JsonDerivedType(typeof(NegativeGoal), typeDiscriminator: "NegativeGoal")]
[JsonDerivedType(typeof(ValueGoal), typeDiscriminator: "ValueGoal")]

class Goal
{
    public string Title {get; set;}
    public string Description {get; set;}
    public int Score {get; set;}
    public int TotalScore {get; set;}
    public bool Status {get; set;}

    public Goal() {}

    public Goal(string title, string desc, int score)
    {
        Title = title;
        Description = desc;
        Score = score;
    }

    public int GetTotalScore()
    {
        return TotalScore;
    }
    public virtual void Award()
    {
        TotalScore += Score;
        Status = true;
        Console.WriteLine($"Congratulations! You have earned {Score} points!");
    }
    public virtual void Display()
    {
        Console.WriteLine(this.GetDisplayString());
    }
    public virtual string GetDisplayString()
    {
        string checkBox;
        if (isChecked())
        {
            checkBox = "[X]";
        }
        else {checkBox = "[ ]";}
        return $"{checkBox} {Title} ({Description})";
    }
    public virtual bool isChecked()
    {
        return Status;
    }
}