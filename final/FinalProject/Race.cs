class Race // TODO: add functionality for random drivers and laps and for loading when it's finished
{
    string _name;
    Track _track;
    List<String> _places = new List<string>();
    List<Driver> _drivers;
    string _weather;
    bool _finished;

    public Race(string name, Track track, List<Driver> drivers, string weather, bool isFinished)
    {
        _name = name;
        _track = track;
        _weather = weather;
        _drivers = drivers;
        _finished = isFinished;
    }

   public Race(string name, Track track, List<Driver> drivers, string weather, bool isFinished, List<String> places)
    {
        _name = name;
        _track = track;
        _weather = weather;
        _drivers = drivers;
        _finished = isFinished;
        _places = places;
    }


    public void StartRace() // TODO: add the start lights logic
    {
        for (float time = 3; time >= 0; time -= 0.01f)
        {
            Console.Clear();
            Console.WriteLine(time);
            Thread.Sleep(10);
        }
        int temp = 0; // Temporary
        do
        {
            Update();
            Thread.Sleep(250);
            temp++; // Temporary
            if (temp >= 500) // Prevents from inf loop for now
            {
                _finished = true;
            }
        }
        while (!_finished);

    }

    public void EndRace()
    {
        Console.Clear();
        Console.WriteLine("Race has finished! Places are:");
        for (int d = 0 ; d < _places.Count() ; d++)
        {
            Console.WriteLine($"{d+1}. {_places[d]}");
        }
    }

    public void Update()
    {
        int driversFinished = _places.Count;
        foreach (Driver d in _drivers)
        {
            if (d.GetCarPosition() >= _track.GetLength() && !_places.Contains(d.GetName()))
            {
                _places.Add(d.GetName());
            }
            else if (!_places.Contains(d.GetName()))
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
            if (driversFinished == _drivers.Count)
            {
                _finished = true;
            }
        }

        DisplayStats();
    }

    public void DisplayStats()
    {
        Console.Clear();
        if (!_finished)
        {
            foreach (Driver d in _drivers)
            {
                Console.WriteLine($"Name: {d.GetName()} Speed: {d.GetSpeed()} Progress: {d.GetProgress()} Position: {d.GetCarPosition()}");
            }
            Console.WriteLine("");
        }
        else{EndRace();}
    }
}