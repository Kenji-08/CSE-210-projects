using System.ComponentModel;

class Reference
{
    private string _book;
    private string _chapter;
    private string _verse;
    private string _endVerse;

    public Reference(string bk, string chpt, string sVerse)
    {
        _book = bk;
        _chapter = chpt;
        _verse = sVerse;
    }
    public Reference(string bk, string chpt, string sVerse, string eVerse)
    {
        _book = bk;
        _chapter = chpt;
        _verse = sVerse;
        _endVerse = "-" + eVerse;
    }

    public void Display()
    {
        Console.WriteLine($"{_book} {_chapter}:{_verse}{_endVerse}");
    }
}