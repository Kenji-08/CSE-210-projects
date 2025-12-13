using System.Runtime.CompilerServices;
using static InputHelper;

class Driver
{
    private string _name;
    private float _reaction;
    int _reactionDelayMs;
    int _reactionTimerMs;
    bool _hasReacted;

    private float _cornerSkill;
    private float _agility;
    private Car _car;
    private int _segmentIndex;
    private float _segmentProgress;
    private float _segmentLengthCrossed;
    private bool _inPit = false;

    public Driver(string name, Car car)
    {
        _name = name;
        _car = car;
        _reaction = 0.5f;
        _segmentIndex = 0;
        _segmentProgress = 0.0f;
        _segmentLengthCrossed = 0;
    }


    public Driver(string name, float reaction, float cornerSkill, float agility, Car car)
    {
        _name = name;
        _reaction = reaction;
        _cornerSkill = cornerSkill;
        _agility = agility;
        _car = car;
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

    public void ReadyReaction()
    {
        // Reaction stat ~ 0.8 – 2.0
        _reactionDelayMs = (int)(600 / _reaction);

        _reactionDelayMs = Math.Clamp(_reactionDelayMs, 150, 700);

        _reactionTimerMs = _reactionDelayMs;
        _hasReacted = false;
    }

    public void Drive(Segment segment)
    {
        float currentSpeed = _car.GetEffectiveSpeed();
        float maxSpeed = segment.GetMaxSpeed();
        bool isCorner = segment.GetType() == typeof(CornerSegment);

        // Car wear
        float speedFactor = _car.GetEffectiveSpeed() * 0.0006f;
        float randomFactor = Random.Shared.NextSingle() * 0.0003f;
        float driverSkill = 1f - (_agility * 0.03f);
        driverSkill = Math.Clamp(driverSkill, 0.7f, 1.05f);
        float wear = (speedFactor + randomFactor) * driverSkill;
        _car.UpdateCondition(-wear);


        if (_car.GetCondition() < 0.2 || _inPit) { EnterPit(); _car.SetCurrentSpeed(0); }

        else if (isCorner)
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

        crashChance = Math.Clamp(crashChance, 0, 1);

        if (Random.Shared.NextDouble() < crashChance)
        {
            // full crash: lose everything
            Console.WriteLine($"{_name} CRASHED in corner!");
            _car.SetPosition(_car.GetPosition() - 50); // knock back
            _car.SetCurrentSpeed(0);
            _car.UpdateCondition(-0.2f);
            _segmentProgress = 0;
        }
        else
        {
            // penalty only (minor oversteer)
            _car.SetCurrentSpeed(seg.GetMaxSpeed());
        }
    }

    public void EnterPit()
    {
        _inPit = true;
        _car.UpdateCondition(0.1f);
        if (_car.GetCondition() >= 1.0f) { _inPit = false; }
    }

    public float GetCarCondition() { return _car.GetCondition(); }

    public bool IsInPit() { return _inPit; }

    public void AllocateSkills()
    {
        Console.Clear();
        Console.WriteLine($"Reaction speed: {_reaction}");
        Console.WriteLine($"Cornering skill: {_cornerSkill}");
        Console.WriteLine($"Agiility: {_agility}");
        Console.WriteLine("You have 4 points");
        for (int i = 1; i < 5; i++)
        {
            string option = Input<string>($"Type R, C, or A for the skill allocation ({i}): ");
            switch (option.ToLower())
            {
                case "r":
                    _reaction += 0.3f;
                    break;
                case "c":
                    _cornerSkill += 0.4f;
                    break;
                case "a":
                    _agility += 0.5f;
                    break;
                default:
                    i -= 1;
                    Console.WriteLine("Not an option please try again");
                    break;
            }
        }
    }

    public void SetCar(Car car) { _car = car; }

    public float[] GetSkills() { return [_reaction, _cornerSkill, _agility]; }

    public void ResetRaceValues()
    {
        _segmentIndex = 0;
        _segmentProgress = 0.0f;
        _segmentLengthCrossed = 0;
        _car.ResetRaceValues();
    }

    public void FinishSegment()
    {
        _segmentProgress = 1.0f;
    }

    public void SetSegmentIndex(int index) { _segmentIndex = index; }

    public void TickReaction(int deltaMs)
    {
        if (_hasReacted) return;

        _reactionTimerMs -= deltaMs;
        if (_reactionTimerMs <= 0)
            _hasReacted = true;
    }

    public bool HasReacted()
    {
        return _hasReacted;
    }
}