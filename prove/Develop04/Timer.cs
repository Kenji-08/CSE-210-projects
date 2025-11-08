using System.Diagnostics;

class Timer
{
    static DateTime _targetTime;

    public void Set(int x)
    {
        _targetTime = DateTime.Now;
        _targetTime = _targetTime.AddSeconds(x);
    }
    public bool IsExpired()
    {
        if (_targetTime.TimeOfDay < DateTime.Now.TimeOfDay)
        {
            return true;
        }
        else{ return false; }
    }
    public void StartCountDown(int x)
    {
        for (int i = x; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    public void PauseWithAnimation(int x)
    {
        for (int i = 0; i < 6; i++)
        {
            Console.Write("-");
            Thread.Sleep(x);
            Console.Write("\b \b");
            Console.Write("\\");
            Thread.Sleep(x);
            Console.Write("\b \b");
            Console.Write("|");
            Thread.Sleep(x);
            Console.Write("\b \b");
            Console.Write("/");
            Thread.Sleep(x);
            Console.Write("\b \b");
        }
        Console.WriteLine("");
    }
    public void PauseWithAnimation(int x, int duration)
    {
        for (int i = 0; i < duration; i++)
        {
            Console.Write("-");
            Thread.Sleep(x);
            Console.Write("\b \b");
            Console.Write("\\");
            Thread.Sleep(x);
            Console.Write("\b \b");
            Console.Write("|");
            Thread.Sleep(x);
            Console.Write("\b \b");
            Console.Write("/");
            Thread.Sleep(x);
            Console.Write("\b \b");
        }
        Console.WriteLine("");
    }
}