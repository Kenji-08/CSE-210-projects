class Tire
{
    string _compound;
    float _grip;
    float _temperature;
    float _wear;

    public Tire(string compound)
    {
        _compound = compound;
        switch (compound)
        {
            case "Soft":
                _grip = 1.2f;
                _wear = 0.7f;
                break;
            case "Medium":
                _grip = 1.0f;
                _wear = 1.0f;
                break;
            case "Hard":
                _grip = 0.8f;
                _wear = 1.3f;
                break;
            default:
                _grip = 1.0f;
                _wear = 1.0f;
                break;
        }
        _temperature = 20.0f; // Ambient temperature
    }

    public float GetGrip()
    {
        return _grip;
    }

    public void Heat(float amount)
    {
        _temperature += amount;
        if (_temperature > 120.0f)
        {
            _temperature = 120.0f; // Max temperature
        }
    }

    public void WearDown(float amount)
    {
        _wear -= amount;
        if(_wear < 0f)
        {
            _wear = 0f;
        }
    }

    public bool IsBad()
    {
        if(_wear < 0.3f) // Subject to change
        {
            return true;
        }
        else{return false;}
    }
}