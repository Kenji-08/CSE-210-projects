using System;

class Program
{
    static void Main(string[] args)
    {
        // Console.WriteLine("Hello Learning03 World!");
        Fraction fract = new Fraction(1);
        Console.WriteLine(fract.GetFractionString());
        Console.WriteLine(fract.GetDecimalValue());

        Fraction fiveFract = new Fraction(5);
        Console.WriteLine(fiveFract.GetFractionString());
        Console.WriteLine(fiveFract.GetDecimalValue());

        Fraction quaterFract = new Fraction(3, 4);
        Console.WriteLine(quaterFract.GetFractionString());
        Console.WriteLine(quaterFract.GetDecimalValue());

        Fraction thirdFract = new Fraction(1,3);
        Console.WriteLine(thirdFract.GetFractionString());
        Console.WriteLine(thirdFract.GetDecimalValue());
    }
}