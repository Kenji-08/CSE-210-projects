using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Please enter the grade percentage: ");
        int PERCENTAGE = int.Parse(Console.ReadLine());
        string letterGrade = getLetterGrade(PERCENTAGE);
        Console.WriteLine($"Your letter grade is {letterGrade}.");
        if (letterGrade == "D" || letterGrade == "F")
        { Console.WriteLine($"You got a 70 or lower this means you fail."); }
        
    }

    static string getLetterGrade(int percentage)
    {
        string letter = "";
        if (percentage >= 90)
        { letter = "A"; }
        else if (percentage >= 80)
        { letter = "B"; }
        else if (percentage >= 70)
        { letter = "C"; }
        else if (percentage >= 60)
        { letter = "D"; }
        else{ letter = "F"; }
        return letter;
    }
}