using System;

class Program
{
    static void Main(string[] args)
    {
        // Math
        Assignment assignment = new Assignment("Benard Smith", "Math");
        Console.WriteLine(assignment.GetSummary());
        MathAssignment mathAssignment = new MathAssignment("7.3", "8-19");
        Console.WriteLine(mathAssignment.GetHomeworkList());

        // Line break
        Console.WriteLine();

        // Writing
        Assignment assignment1 = new Assignment("Barry Donald", "East Asian History");
        Console.WriteLine(assignment1.GetSummary());
        WritingAssignment writingAssignment = new WritingAssignment("The Art of War", "Sun Tzu");
        Console.WriteLine(writingAssignment.GetWritingInformation());
    }
}