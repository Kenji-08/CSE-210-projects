using System.Dynamic;

class MathAssignment : Assignment
{
    private string _textBookSection;
    private string _problems;


    public MathAssignment(string section, string problems) : base(section, problems)
    {
        _textBookSection = section;
        _problems = problems;
    }
    public string GetHomeworkList()
    {
        return $"Section {_textBookSection} Problems {_problems}";
    }
}