class Assignment
{
    // attributes
    protected string _studentName;
    private string _topic;


    // methods
    public Assignment(string name, string topic)
    {
        _studentName = name;
        _topic = topic;
    }
    public string GetSummary()
    {
        return $"{_studentName} - {_topic}";
    }
}