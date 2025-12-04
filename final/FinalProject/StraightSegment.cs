class StraightSegment : Segment
{
    public StraightSegment(float length, int index) : base(length, index)
    {
    }

    override public float GetSpeedModifier()
    {
        return 1.0f;
    }

    override public float GetOvertakeMod()
    {
        return 1.0f;
    }
}