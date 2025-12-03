class CornerSegment : Segment
{
    float _difficulty;

    public CornerSegment(float length, int index, float difficulty) : base(length, index)
    {
        _difficulty = difficulty;
    }

    override public float GetSpeedModifier()
    {
        return 1.0f - _difficulty;
    }

    override public float GetOvertakeMod()
    {
        return 0.5f * (1.0f - _difficulty);
    }
}