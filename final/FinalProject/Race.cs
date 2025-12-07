class Race
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

    public void StartRace()
    {
        
    }

    public void EndRace()
    {
        
    }

    public void Update()
    {
        
    }

    public void DisplayStats()
    {
        
    }
}