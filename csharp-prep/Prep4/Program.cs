// Assignment
// Ask the user for a series of numbers, and append each one to a list. 
// Stop when they enter 0.

// Once you have a list, have your program do the following:

// Core Requirements
// Work through these core requirements step-by-step to complete 
// the program. Please don't skip ahead and do the whole thing at 
// once, because others on your team may benefit from building 
// the program up slowly.

// Compute the sum, or total, of the numbers in the list.

// Compute the average of the numbers in the list.

// Find the maximum, or largest, number in the list.

using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> intList = new List<int>();
        int sum = 0;
        double average = 0;
        int max = 0;
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        int input = 1;
        do
        {
            Console.Write("Enter a number: ");
            input = Convert.ToInt32(Console.ReadLine());
            intList.Add(input);
        } while (input != 0);

        foreach (int num in intList)
        {
            sum += num;
            if (num > max) { max = num; }
        }
        average = Convert.ToDouble(sum) / Convert.ToDouble(intList.Count - 1);

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");        
    }
}