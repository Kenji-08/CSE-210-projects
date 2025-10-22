using System.ComponentModel.DataAnnotations;

class Scripture
{
    Reference _reference;
    string _text;
    bool _IsAllHidden = false;
    List<Word> _words = new List<Word>();

    public Scripture(Reference refer, string txt)
    {
        _reference = refer;
        _text = txt;

        string[] wrds = txt.Split(' ');

        foreach (string wrd in wrds)
        {
            Word newWord = new Word(wrd);
            _words.Add(newWord);
        }
    }

    public void HideWords()
    {
        bool run = true;
        do // makes sure a word is set to hidden
        {
            if (!IsAllHidden())
            {
                Random random = new Random();
                int r = random.Next(_words.Count);
                if (_words[r].IsShown())
                {
                    _words[r].Hide();
                    run = false;
                }
            }
            else run = false;
        } while (run);

    }

    public bool IsAllHidden()
    {
        int hiddenCount = 0;
        foreach (Word wrd in _words)
        {
            if (!wrd.IsShown())
            {
                hiddenCount++;
            }
        }
        if (hiddenCount == _words.Count())
        {
            _IsAllHidden = true;
            return true;
        }
        else
        {
            _IsAllHidden = false;
            return false;
        }
    }

    public bool IsShown(Word wrd)
    {
        return wrd.IsShown();
    }
    public void Display()
    {
        _reference.Display();
        Console.WriteLine(_text);
    }

    public void DisplayRender()
    {
        _reference.Display();
        foreach (Word wrd in _words)
        {
            wrd.Display();
        }
        Console.WriteLine("");
    }
}