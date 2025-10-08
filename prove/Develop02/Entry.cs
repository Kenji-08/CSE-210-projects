using System.ComponentModel.DataAnnotations;

class Entry
{
    // attributes
    public string _givenPrompt;
    public string _date;
    public string _entryText;
    public string _time;
    public string _mood;

    // behavior
    public void Display()
    {
        Console.WriteLine($"Date & time: {_date} {_time}\n Prompt: {_givenPrompt}\n Mood: {_mood}\n Entry: {_entryText}\n");
    }
}