class Car
{
    string _id;
    string _name;
    float _speed;
    float _currentSpeed;
    float _acceleration;
    float _topSpeed;
    float _condition;
    float _brakes;
    Tire _tire;
    float _position;

    public Car(string id, string name, float speed, float topSpeed, float acceleration, float brakes, Tire tire)
    {
        _id = id;
        _name = name;
        _speed = speed;
        _acceleration = acceleration;
        _topSpeed = topSpeed;
        _condition = 1.0f; // New car condition
        _currentSpeed = 0f;
        _position = 0;
        _tire = tire;
        _brakes = brakes;
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

    public float GetCondition() { return _condition; }

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

    public void SetCurrentSpeed(float amt) { _currentSpeed = amt; }

    public void Accelerate(float segmentMod)
    {
        float accelForce = _acceleration * _tire.GetGrip() * _condition * segmentMod;
        _currentSpeed += accelForce;
        _position += _currentSpeed;
        if (_currentSpeed > _topSpeed)
        {
            _currentSpeed = _topSpeed;
        }
    }

    public void Deccelerate()
    {
        float brakeForce = _brakes * _tire.GetGrip() * _condition;
        _currentSpeed -= brakeForce;

        _currentSpeed = Math.Max(_currentSpeed, 0);
        _position += _currentSpeed;
    }

    public string GetStats()
    {
        return $"{_name}:\n\tID: {_id}\n\tSpeed(power): {_speed}\n\tBrakes: {_brakes}\n\tAcceleration: {_acceleration}";
    }

    public void ResetRaceValues()
    {
        _condition = 1.0f;
        _currentSpeed = 0f;
        _position = 0;
    }
}