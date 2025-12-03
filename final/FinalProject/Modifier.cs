class Modifier
{
    private float _speedMult;
    private float _agilityMult;

    public Modifier(float speedMult, float agilityMult)
    {
        _speedMult = speedMult;
        _agilityMult = agilityMult;
    }

    public void ApplyTo(Driver driver)
    {
        driver.ApplySkillModifiers();
    }

    public float GetSpeedMult()
    {
        return _speedMult;
    }

    public void SetSpeedMult(float speedMult)
    {
        _speedMult = speedMult;
    }

    public float GetAgilityMult()
    {
        return _agilityMult;
    }

    public void SetAgilityMult(float agilityMult)
    {
        _agilityMult = agilityMult;
    }

}