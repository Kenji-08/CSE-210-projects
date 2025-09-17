using System;

class Program
{
    static void Main(string[] args)
    {
        getAndDisplayName();
    }

    static void getAndDisplayName()
    {
        Console.Write("What is your first name? ");
        string firstName = Console.ReadLine();
        Console.Write("What is you last name? ");
        string lastName = Console.ReadLine();

        Console.WriteLine($"Your name is {lastName}, {firstName} {lastName}");
    }
}