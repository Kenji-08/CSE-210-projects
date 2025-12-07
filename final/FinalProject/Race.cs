class Race // TODO: add functionality for random drivers
{
    string _name;
    Track _track;
    List<Driver> _podium;
    List<Driver> _drivers;
    string _weather;

    public Race(string name, Track track, List<Driver> drivers)
    {
        _name = name;
        _track = track;
        _weather = "Clear";
        _drivers = drivers;
    }

    public Race(string name, Track track, List<Driver> podium, string weather)
    {
        _name = name;
        _track = track;
        _weather = weather;
    }


    public void StartRace() // TODO: add the start lights logic
    {
        for (float time = 1; time >= 0; time -= 0.01f)
        {
            Console.WriteLine(time);
            Thread.Sleep(1);
        }
        bool end = false;
        int temp = 0; // Temporary
        do
        {
            Update();
            Thread.Sleep(500);
            temp++; // Temporary
            if (temp >= 20)
            {
                end = true;
            }
        }
        while (!end);

    }

    public void EndRace()
    {

    }

    public void Update()
    {
        foreach (Driver d in _drivers)
        {
            Segment segment = _track.GetSegment(d.GetSegmentIndex());
            if (d.GetProgress() > 1.0f)
            {
                d.AddSegmentLengthCrossed(segment.GetLength());
                segment = _track.GetNextSegment(d.GetSegmentIndex());
                d.IncrementSegmentIndex();
            }
            d.Drive(segment);
        }

        DisplayStats();
    }

    public void DisplayStats()
    {
        Console.Clear();
        foreach (Driver d in _drivers)
        {
            Console.WriteLine($"Name: {d.GetName()} Speed: {d.GetSpeed()} Progress: {d.GetProgress()} Position: {d.GetCarPosition()}");
        }
        Console.WriteLine("");
    }
}