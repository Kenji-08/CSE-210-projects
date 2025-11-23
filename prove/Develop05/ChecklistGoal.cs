using System.Text.Json.Serialization;

class ChecklistGoal : Goal
{
    public int TimesAwarded {get; set;}
    public int Bonus {get; set;}
    public bool GotBonus {get; set;}
    public int MaxAwards {get; set;}

    public ChecklistGoal(){}
    public ChecklistGoal(string title, string desc, int score, int bonus, int maxAwards) : base(title, desc, score)
    {
        Bonus = bonus;
        MaxAwards = maxAwards;
        GotBonus = false;
    }

    public ChecklistGoal(string title, string desc, int score, int timesAwarded, int bonus, bool gotBonus, int maxAwards) : base(title, desc, score)
    {
        TimesAwarded = timesAwarded;
        Bonus = bonus;
        GotBonus = gotBonus;
        MaxAwards = maxAwards;
    }

    public override void Award()
    {
        base.Award();
        Status = false;
        if (TimesAwarded < MaxAwards) {TimesAwarded++;}
        if (TimesAwarded == MaxAwards)
        {
            Status = true;
            if (!GotBonus)
            {
                TotalScore += Bonus;
                GotBonus = true;
            }
        }
    }

    public override string GetDisplayString()
    {
        return  $"{base.GetDisplayString()} -- Currently completed: {TimesAwarded}/{MaxAwards}";
    }
}