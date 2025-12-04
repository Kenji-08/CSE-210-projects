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

    public void Drive(Segment segment)
    {
        
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