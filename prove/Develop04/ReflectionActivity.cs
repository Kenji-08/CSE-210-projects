using System.Globalization;

class ReflectionActivity : Activity
{
    private List<string> _prompts = ["Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."];
    private List<string> _questions = ["How did you feel when this experience was complete? ",
        "What strengths did you exhibit during this experience? ",
        "How can you apply what you learned from this experience in the future? ",
        "What did you learn about yourself through this experience? ",
        "How did this experience change your perspective? "];
    
    // Copy of the lists
    private List<string> _notPrompted;
    private List<string> _notAskedQuestions;

    private string _prompt;
    private string _question;
    private Random _rand = new Random();

    public ReflectionActivity(string title, string desc) : base(title, desc)
    {
        _notPrompted = _prompts;
        _notAskedQuestions = _questions;
    }
    public void InteractReflection()
    {
        InteractPrologue();
        int duration = GetDuration() / 2;
        int index = _rand.Next(_notPrompted.Count());

        Console.WriteLine("Consider the following prompt:");
        _prompt = _notPrompted[_rand.Next(index)];
        _notPrompted.RemoveAt(index);

        Console.WriteLine($"\n --- {_prompt} ---");
        Console.WriteLine("When you have something in mind, press enter to continue");
        Console.ReadLine();
        Console.Write("You may begin in: ");
        Timer timer = new Timer();
        timer.StartCountDown(5);
        Console.Clear();

        for (int i = 0; i < 2; i++)
        {
            index = _rand.Next(_notAskedQuestions.Count());
            _question = _notAskedQuestions[index];

            // Removes the question from the unasked questions.
            for (int j = 0; j < _notAskedQuestions.Count(); j++)
            {
                if (_notAskedQuestions[j] == _question)
                {
                    _notAskedQuestions.RemoveAt(index);
                }
            }

            Console.Write(_question);

            // Makes anim 1 second each pass.
            timer.PauseWithAnimation(1000 / 4, duration);
        }

        Console.WriteLine();
        InteractEpilogue();
    }
}