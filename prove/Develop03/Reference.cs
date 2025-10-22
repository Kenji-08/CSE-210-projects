using System.ComponentModel;

class Reference
{
    string _book;
    string _chapter;
    string _verse;
    string _endVerse;

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

    public string GetFull()
    {
        return $"{_book} {_chapter}:{_verse}{_endVerse}";
    }

    public void Display()
    {
        Console.WriteLine($"{_book} {_chapter}:{_verse}{_endVerse}");
    }
}