using System;

class Journal
{
    // attribute
    public List<Entry> _entryList = new List<Entry>();

    // behaviors
    public void AppendEntry(Entry e)
    {
        _entryList.Add(e);
    }
    
    public void Display()
    {
        foreach (Entry entry in _entryList)
        {
            Console.WriteLine("---------------------------");
            entry.Display();
        }
    }
}