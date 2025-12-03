abstract class Segment
{
    float _length;
    int _index;

    public Segment(float length, int index)
    {
        _length = length;
        _index = index;
    }

    abstract public float GetSpeedModifier();
    abstract public float GetOvertakeMod();
}