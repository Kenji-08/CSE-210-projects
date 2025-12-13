using System.ComponentModel;

class Race
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


    public void StartRace(int tickMs, int displayMs)
    {
        // Reset all driver stuff from last race
        foreach (Driver d in _drivers)
        {
            d.ResetRaceValues();
            d.ReadyReaction();
        }

        long tick = 0;
        int second = 1000;
        int minute = second * 60;
        for (float time = 3; time >= 0; time -= 0.01f)
        {
            Console.Clear();
            Console.WriteLine($"The {_name} starts in...");
            Console.WriteLine(Math.Round(time, 2));
            Thread.Sleep(10);
        }

        do
        {
            Update(tickMs);
            tick += tickMs;
            Thread.Sleep(tickMs);

            if (tick % displayMs == 0)
            {
                DisplayStats();
            }

            if (tick >= minute * 5) // Prevents from inf loop
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
        InputHelper.Input<string>("Press enter to continue ");
    }

    public void Update(int tickMs)
    {
        foreach (Driver d in _drivers)
        {
            if (!d.HasReacted())
            {
                d.TickReaction(tickMs);
                continue; // driver does nothing yet
            }

            if (d.GetCarPosition() >= _track.GetLength() * _track.GetLaps() && !_places.Contains(d.GetName()))
            {
                _places.Add(d.GetName());
            }
            else if (!_places.Contains(d.GetName()))
            {
                Segment segment = _track.GetSegment(d.GetSegmentIndex());
                if (d.GetProgress() > 1.0f)
                {
                    d.AddSegmentLengthCrossed(segment.GetLength());

                    if (_track.HasNextSegment(d.GetSegmentIndex()))
                    {
                        d.IncrementSegmentIndex();
                        segment = _track.GetSegment(d.GetSegmentIndex());
                    }
                    else
                    {
                        // wrap back to first segment
                        d.SetSegmentIndex(0);
                        segment = _track.GetSegment(0);
                    }

                }

                d.Drive(segment);
            }
            if (_places.Count >= _drivers.Count)
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
            Console.WriteLine($"{_name}\nWeather: {_weather}");
            foreach (Driver d in _drivers)
            {
                double speed = Math.Round(d.GetSpeed(), 2);
                double pos = Math.Round(d.GetCarPosition(), 2);
                int lap = ((int)pos / (int)_track.GetLength()) + 1;
                Console.WriteLine($"{d.GetName()} Speed: {speed} Lap: {lap} Position: {pos}");
                if (d.IsInPit()) { Console.WriteLine("\tIs in the pit!"); }
            }
            Console.WriteLine("");
        }
        else { EndRace(); }
    }

    public string GetName() { return _name; }
    public bool IsFinished() { return _finished; }
    public List<string> GetPlaces() { return _places; }
    public List<string> GetPodium()
    {
        int count = Math.Min(3, _places.Count);
        return _places.GetRange(0, count);
    }

    public int GetDriverPlace(string name)
    {
        int place = 0;
        for (int d = 0; d < _places.Count(); d++)
        {
            if (_places[d] == name)
            {
                place = d + 1;
            }
        }

        return place;
    }
}