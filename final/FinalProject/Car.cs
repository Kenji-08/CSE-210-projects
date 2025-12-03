class Car
{
    float _speed;
    float _acceleration;
    float _condition;
    Tire _tire;
    
    public Car(float speed, float acceleration, Tire tire)
    {
        _speed = speed;
        _acceleration = acceleration;
        _condition = 1.0f; // New car condition
        _tire = tire;
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

    public float GetSpeed()
    {
        return _speed * _condition * _tire.GetGrip(); // May change
    }
}