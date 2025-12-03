class Race
{
    string _name;
    Track _track;
    int _distance;
    List<Driver> _podium;
    List<Driver> _drivers;
    string _weather;

    public Race(string name, Track track, int distance, List<Driver> drivers)
    {
        _name = name;
        _track = track;
        _distance = distance;
        _weather = "Clear";
        _drivers = drivers;
    }

    public Race(string name, Track track, int distance, List<Driver> podium, string weather)
    {
        _name = name;
        _track = track;
        _distance = distance;
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