class Driver
{
    private string _name;
    private float _reaction;
    private float _cornerSkill;
    private float _agility;
    private Car _car;
    private int _currentPlace;
    private int _segmentIndex;
    private float _segmentProgress;
    private float _segmentLengthCrossed;

    public Driver(string name, Car car)
    {
        _name = name;
        _car = car;
        _reaction = 0.5f; // Subject to change based on balancing
        _currentPlace = 0;
        _segmentIndex = 0;
        _segmentProgress = 0.0f;
    }

    // Loading or testing only
    public Driver(string name, float reaction, float cornerSkill, float agility, Car car)
    {
        _name = name;
        _reaction = reaction;
        _cornerSkill = cornerSkill;
        _agility = agility;
        _car = car;
        _currentPlace = 0;
        _segmentIndex = 0;
        _segmentProgress = 0.0f;
    }

    public int GetSegmentIndex()
    {
        return _segmentIndex;
    }

    public void IncrementSegmentIndex()
    {
        _segmentIndex++;
    }

    public float GetSpeed()
    {
        return _car.GetEffectiveSpeed();
    }

    public float GetProgress()
    {
        return _segmentProgress;
    }

    public void SetProgress(float progress)
    {
        _segmentProgress = progress;
    }

    public float GetCarPosition()
    {
        return _car.GetPosition();
    }

    public string GetName()
    {
        return _name;
    }

    public void AddSegmentLengthCrossed(float len)
    {
        _segmentLengthCrossed += len;
    }

    public void Drive(Segment segment)
    {
        if (_segmentProgress < 0.80f)
        {
            _car.Accelerate(segment.GetSpeedModifier());
        }
        else { _car.Deccelerate();}
        _segmentProgress = (_car.GetPosition()-_segmentLengthCrossed) / segment.GetLength();
    }

    public void AttemptOvertake(Driver other)
    {

    }

    public void EnterPit()
    {

    }

    public void ApplySkillModifiers()
    {

    }
}