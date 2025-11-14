using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;

class Program
{
    static void Main(string[] args)
    {
        // Console.WriteLine("Hello Learning05 World!");

        List<Shape> shapes = new List<Shape>();

        Square square = new Square("blue", 3);
        Rectangle rect = new Rectangle("red", 8, 5);
        Circle circle = new Circle("green", 7);
        shapes.Add(square);
        shapes.Add(rect);
        shapes.Add(circle);

        for (int i = 0; i < shapes.Count; i++)
        {
            Console.WriteLine($"Shape: {shapes[i].GetType()}\n\tColor: {shapes[i].GetColor()}\n\tArea: {shapes[i].GetArea()}");
        }
    }
}