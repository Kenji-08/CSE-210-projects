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


    public void StartRace() // TODO: add the start lights logic and make it prettier
    {
        // Reset all driver stuff from last race
        foreach (Driver d in _drivers)
        {
            d.ResetRaceValues();
        }
        long tick = 0;
        int second = 1000;
        int minute = second * 60;
        for (float time = 3; time >= 0; time -= 0.01f)
        {
            Console.Clear();
            Console.WriteLine($"The {_name} starts in...");
            Console.WriteLine(Math.Round(time,2));
            Thread.Sleep(10);
        }
       
        do
        {
            tick += 250;
            Update();
            DisplayStats();
            Thread.Sleep(250);

            if (tick >= minute) // Prevents from inf loop
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
        for (int d = 0; d < _places.Count(); d++)
        {
            Console.WriteLine($"{d + 1}. {_places[d]}");
        }
    }

    public void Update() // TODO: add reactionTime functionality
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

    }

    public void DisplayStats()
    {
        Console.Clear();
        if (!_finished)
        {
            foreach (Driver d in _drivers)
            {
                Console.WriteLine($"Name: {d.GetName()} Speed: {Math.Round(d.GetSpeed(), 2)} Position: {Math.Round(d.GetCarPosition(), 2)} Condition: {Math.Round(d.GetCarCondition(), 2)}");
                if (d.IsInPit()) { Console.WriteLine("\tIs in the pit!"); }


            }
            Console.WriteLine("");
        }
        else { EndRace(); }
    }

    public string GetName(){ return _name;}
    public bool IsFinished(){ return _finished;}
    public List<string> GetPlaces(){return _places;}
    public List<string> GetPodium(){return _places.GetRange(0,3);}
    public int GetDriverPlace(string name)
    {
        int place = 0;
        for (int d = 0 ; d < _places.Count() ; d++)
        {
            if (_places[d] == name)
            {
                place = d+1;
            }
        }

        return place;
    }
}