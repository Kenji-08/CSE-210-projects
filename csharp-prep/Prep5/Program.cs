// For this assignment, write a C# program that has several simple functions:

// DisplayWelcome - Displays the message, "Welcome to the Program!"
// PromptUserName - Asks for and returns the user's name (as a string)
// PromptUserNumber - Asks for and returns the user's favorite number 
// (as an integer)
// PromtUserBirthYear - Accepts out integer parameter and prompts the 
// user for the year they were born. The out parameter is set to their 
// birth year. This function does not return a value. The user's birth 
// year is given back from the function via the out parameter.
// SquareNumber - Accepts an integer as a parameter and returns that 
// number squared (as an integer)
// DisplayResult - Accepts the user's name, the squared number, and 
// the user's birth year. Display the user's name and squared number. 
// Calculate hold many years old they will turn this year and display that.
// Your Main function should then call each of these functions saving 
// the return values and passing data to them as necessary.

using System;

class Program
{
    static void Main(string[] args)
    {
        string name;
        int birthYear;
        int num;
        int squaredNum;

        DisplayWelcome();
        name = PromptUserName();
        num = PromptUserNumber();
        PromptUserBirthYear(out birthYear);
        squaredNum = SquareNumber(num);
        DisplayResult(name, squaredNum, birthYear);

    }

    static void DisplayWelcome() { Console.WriteLine("Welcome to the Program!"); }

    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();
        return name;
    }

    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        return Convert.ToInt32(Console.ReadLine());
    }

    static void PromptUserBirthYear(out int x)
    {
        Console.Write("Please enter the year you were born: ");
        x = Convert.ToInt32(Console.ReadLine());
    }

    static int SquareNumber(int x)
    {
        return Convert.ToInt32(Math.Pow(Convert.ToDouble(x), 2.0));
    }

    static void DisplayResult(string name, int square, int year)
    {
        Console.WriteLine($"{name}, the square of you number is {square}");
        Console.WriteLine($"{name}, you will turn {2025-year} this year.");
    }
    
}