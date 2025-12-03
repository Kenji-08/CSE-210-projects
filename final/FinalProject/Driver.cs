class Driver
{
    string _name;
    float _reaction;
    float _cornerSkill;
    float _agility;
    Car _car;
    int _currentPlace;
    int _segmentIndex;
    float _segmentProgress;

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

    public void AttemptOVertake(Driver other)
    {
        
    }

    public void EnterPit()
    {
        
    }

    public void ApplySkillModifiers()
    {
        
    }
}