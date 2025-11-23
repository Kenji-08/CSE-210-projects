using System.Text.Json.Serialization;

class ValueGoal : Goal
{
    public int CurrentNumber {get; set;}
    public int TargetNumber {get; set;}

    public ValueGoal(){}

    [JsonConstructor]    
    public ValueGoal(string title, string desc, int score, int target, int current, int Target) : base(title, desc, score)
    {
        CurrentNumber = current;
        TargetNumber = Target;
    }

    public ValueGoal(string title, string desc, int score, int target) : base(title, desc, score)
    {
        TargetNumber = target;
        CurrentNumber = 0;
    }

    public override void Award()
    {
        Console.WriteLine("What value do you achieve? ");
        CurrentNumber = Convert.ToInt32(Console.ReadLine());
        if(CurrentNumber == TargetNumber)
        {
            base.Award();
        }
        else if(CurrentNumber < TargetNumber){Console.WriteLine("Your currently under the goal, you got this!");}
        else if(CurrentNumber > TargetNumber){Console.WriteLine("Your currently over the goal, you got this!");}
    }

    public override string GetDisplayString()
    {
        return $"{base.GetDisplayString()} -- Latest entry: {CurrentNumber}/{TargetNumber}";
    }
}