class Track
{
    string _name;
    List<Segment> _segments;
    float _length;
    int _laps;

    public Track(string name, int laps, float length, List<Segment> segments)
    {
        _name = name;
        _laps = laps;
        _segments = segments;
        _length = length;
    }

    public Segment GetNextSegment(int currentIndex)
    {
        return _segments[currentIndex+1];
    }

    public Segment GetSegment(int index)
    {
        return _segments[index];
    }

    public float GetLength(){return _length;}
}