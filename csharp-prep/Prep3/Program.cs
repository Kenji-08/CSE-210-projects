using System;

class Program
{
    static void Main(string[] args)
    {
        int guess = 0;

        Console.Write("What is the magic number? ");
        Random randomNumber = new Random();
        int magicNumber = randomNumber.Next(1, 101);

        do
        {
            Console.Write("What is your guess? ");
            guess = Convert.ToInt32(Console.ReadLine());
            if (guess > magicNumber)
            {
                Console.WriteLine("Number is too high");
            }
            else if (guess < magicNumber)
            {
                Console.WriteLine("Number is too low");
            }
        } while (guess != magicNumber);
        Console.WriteLine("You guessed it!");

    }
}