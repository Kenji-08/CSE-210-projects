class Car
{
    int _id;
    string _name;
    float _speed;
    float _currentSpeed;
    float _acceleration;
    float _topSpeed;
    float _condition;
    Tire _tire;
    
    public Car(int id, string name, float speed, float topSpeed, float acceleration, Tire tire)
    {
        _id = id;
        _name = name;
        _speed = speed;
        _acceleration = acceleration;
        _topSpeed = topSpeed;
        _condition = 1.0f; // New car condition
        _tire = tire;
        _currentSpeed = 0f;
    }

    public void UpdateCondition(float amount)
    {
        _condition += amount;
        if (_condition > 1.0f)
        {
            _condition = 1.0f;
        }
        else if (_condition < 0.0f)
        {
            _condition = 0.0f;
        }
    }

    public void ChangeTires(Tire newTire)
    {
        _tire = newTire;
    }

    public float GetEffectiveSpeed(Segment segment)
    {
        _currentSpeed += _speed * _condition * _tire.GetGrip() * _acceleration * segment.GetSpeedModifier(); // May change
        if (_currentSpeed > _topSpeed){return _topSpeed;}
        else {return _currentSpeed;}
    }

    public void Accelerate(float amount)
    {
        _acceleration = amount;
    }
}