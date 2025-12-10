class Segment
{
    protected float _length;
    protected int _index;

    public Segment(float length, int index)
    {
        _length = length;
        _index = index;
    }

    virtual public float GetSpeedModifier() { return 1f; }
    virtual public float GetOvertakeMod() { return 0f; }
    public float GetLength()
    {
        return _length;
    }
    virtual public float GetMaxSpeed() { return 0f; }
    virtual public float GetDifficulty() { return 0f; }
}