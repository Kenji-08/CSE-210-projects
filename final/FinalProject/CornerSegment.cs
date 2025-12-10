class CornerSegment : Segment
{
    float _difficulty;
    float _maxSpeed;

    public CornerSegment(float length, int index, float difficulty, float maxSpeed) : base(length, index)
    {
        _difficulty = difficulty;
        _maxSpeed = maxSpeed;
    }

    override public float GetSpeedModifier()
    {
        return 1.0f - _difficulty;
    }

    override public float GetOvertakeMod()
    {
        return 0.5f * (1.0f - _difficulty);
    }

    override public float GetMaxSpeed() { return _maxSpeed; }

    public float GetDifficulty() { return _difficulty; }
}