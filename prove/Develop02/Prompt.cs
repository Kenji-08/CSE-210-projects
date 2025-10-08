using System;

class Prompt
{
    // attributes
    public Random _randomProducer = new Random();
    public String[] _prompts =
    [
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What is something in the last 24 hours that I'm grateful for",
        "What was the feat of today",
        "What\'s one thing I\'ve learned today",
        "What can I improve on"
    ];
    public int _selectedPromptIndex;

    // behavior
    public string GeneratePrompt()
    {
        _selectedPromptIndex = _randomProducer.Next(_prompts.Length);
        return _prompts[_selectedPromptIndex];
    }
}