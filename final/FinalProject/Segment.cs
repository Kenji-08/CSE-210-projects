class Segment
{
    protected float _length;
    protected int _index;

    public Segment(float length, int index)
    {
        _length = length;
        _index = index;
    }

    virtual public float GetSpeedModifier(){return 0;}
    virtual public float GetOvertakeMod(){return 0;}
}