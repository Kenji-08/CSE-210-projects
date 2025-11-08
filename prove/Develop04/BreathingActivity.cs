class BreathingActivity : Activity
{
    public BreathingActivity(string title, string desc) : base(title, desc)
    {

    }
    public void InteractBreathing()
    {
        InteractPrologue();
        int duration = GetDuration() / 10;

        for (int i = 0; i < duration; i++)
        {
            Console.Write("Breath in...");
            for (int j = 4; j > 0; j--)
            {
                Console.Write(j);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
            Console.WriteLine();
            Console.Write("Now Breath out...");
            for (int j = 6; j > 0; j--)
            {
                Console.Write(j);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
        InteractEpilogue();
    }
}