class Car // TODO: balance speed
{
    string _id;
    string _name;
    float _speed;
    float _currentSpeed;
    float _acceleration;
    float _topSpeed;
    float _condition;
    Tire _tire;
    float _position;

    public Car(string id, string name, float speed, float topSpeed, float acceleration, Tire tire)
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

    public float GetEffectiveSpeed()
    {
        return _currentSpeed;
    }

    public float GetPosition()
    {
        return _position;
    }

    public void SetPosition(float num)
    {
        _position = num;
    }

    public void Accelerate(float segmentMod)
    {
        _currentSpeed += (float)Math.Pow(Convert.ToDouble(_speed * _condition * _tire.GetGrip() * segmentMod), Convert.ToDouble(_acceleration)); // May change
        _position += _currentSpeed;
        if (_currentSpeed > _topSpeed)
        {
            _currentSpeed = _topSpeed;
        }
    }

    public void Deccelerate()
    {
        _currentSpeed -= 1 / _currentSpeed;
        _position += _currentSpeed;
    }
}