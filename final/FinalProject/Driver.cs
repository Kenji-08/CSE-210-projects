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
        float currentSpeed = _car.GetEffectiveSpeed();
        float maxSpeed = segment.GetMaxSpeed();
        bool isCorner = segment.GetType() == typeof(CornerSegment);

        if (isCorner)
        {
            if (currentSpeed > maxSpeed)
            {
                TryCrash(currentSpeed - maxSpeed, segment, this);
                return;
            }
            else if (currentSpeed < maxSpeed * 0.9f)
            {
                // accelerate up to acceptable cornering speed
                _car.Accelerate(segment.GetSpeedModifier());
            }
            else
            {
                // lightly brake
                _car.Deccelerate();
            }
        }
        else
        {
            // full accelerate on straights
            _car.Accelerate(segment.GetSpeedModifier());
        }

        // update progress
        _segmentProgress = (_car.GetPosition() - _segmentLengthCrossed)
                           / segment.GetLength();
    }


    public void TryCrash(float overshoot, Segment seg, Driver driver)
    {
        // The more they overshoot, the higher the chance they spin out.
        float difficulty = seg.GetDifficulty(); // higher = harder
        float baseChance = overshoot / seg.GetMaxSpeed();  // scaled
        float driverSkill = _cornerSkill;       // 0–1, higher is better

        float crashChance = baseChance * difficulty * (1 - driverSkill);

        // clamp crashChance to 0..1
        crashChance = Math.Clamp(crashChance, 0, 1);

        if (Random.Shared.NextDouble() < crashChance)
        {
            // full crash: lose everything
            Console.WriteLine($"{_name} CRASHED in corner!");
            _car.SetPosition(_car.GetPosition() - 50); // knock back
            _car.SetCurrentSpeed(0);
            _segmentProgress = 0;
        }
        else
        {
            // penalty only (minor oversteer)
            _car.SetCurrentSpeed(seg.GetMaxSpeed());
        }
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