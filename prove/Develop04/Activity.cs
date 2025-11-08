class Activity
{
    private string _title;
    private string _desc;
    private int _duration;

    public Activity(string title, string desc)
    {
        _title = title;
        _desc = desc;
    }
    public int GetDuration()
    {
        return _duration;
    }

    public void InteractPrologue()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_title} Activity");
        Console.WriteLine(_desc);
        Console.WriteLine($"\nHow long, in seconds, would you like your session? ");
        _duration = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        Console.WriteLine("Get Ready...");
        Timer timer = new Timer();
        timer.PauseWithAnimation(100);
    }
    public void InteractEpilogue()
    {
        Console.WriteLine("Well Done!");
        Timer timer = new Timer();
        timer.PauseWithAnimation(100);
        Console.WriteLine($"You have completed another {_duration}");
        timer.PauseWithAnimation(100);
        Console.Clear();
    }
}