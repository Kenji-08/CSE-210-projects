class Word
{
    private string _word;
    private bool _shown;

    public Word(String w) // setter
    {
        _word = w;
        _shown = true;
    }
    public void Hide() // setter
    {
        _shown = false;
    }
    public void Show() // setter
    {
        _shown = true;
    }
    public bool IsShown() // getter
    {
        return _shown;
    }
    public void Display()
    {
        if (_shown)
        {
            Console.Write(_word + " ");
        }
        else
        {
            // Console.Write(" ");
            for (int i = 0; i < _word.Length; i++)
                Console.Write("_");

            Console.Write(" ");
        }
    }
}