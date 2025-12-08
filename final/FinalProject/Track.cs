class Track
{
    string _type;
    List<Segment> _segments;
    float _length;

    public Track(string type, List<Segment> segments, float length)
    {
        _type = type;
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