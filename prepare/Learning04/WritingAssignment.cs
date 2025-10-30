using System.Net;

class WritingAssignment : Assignment
{
    private string _title;
    private string _author;
    public WritingAssignment(string title, string author) : base(title, author)
    {
        _title = title;
        _author = author;
    }
    public string GetWritingInformation()
    {
        return $"{_title} by {_author}";
    }
}