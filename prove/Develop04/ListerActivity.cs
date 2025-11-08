class ListerActivity : Activity
{
    private List<string> _prompts = ["When have you felt the Holy Ghost this month?",
        "Who are people that you love?",
        "What are personal strengths you have?",
        "Who are people that you have helped this week?",
        "What are things that make you happy?"];
    private string _prompt;

    // Copy of the list
    private List<string> _notPrompted;
    private int _itemCount;
    private Random _rand = new Random();

    public ListerActivity(string title, string desc) : base(title, desc)
    {
        _notPrompted = _prompts;
    }
    public void InteractLister()
    {
        InteractPrologue();
        int index = _rand.Next(_prompts.Count());

        Console.WriteLine("Write as many responses you can to the following prompt:");
        _prompt = _notPrompted[index];
        _notPrompted.RemoveAt(index);

        Console.WriteLine($"--- {_prompt} ---");
        Timer timer = new Timer();
        timer.Set(GetDuration());
        while (!timer.IsExpired())
        {
            Console.ReadLine();
            _itemCount++;
        }

        Console.WriteLine($"You listed {_itemCount} items!");
        Console.WriteLine();
        InteractEpilogue();
    }
}