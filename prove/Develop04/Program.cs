using System;

class Program
{
    static void Main(string[] args)
    {
        MenuLoop();
    }

    static void MenuLoop()
    {
        int input = 0;
        while (input != 4)
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("\t1. Start Breathing Activity");
            Console.WriteLine("\t2. Start Reflecting Activity");
            Console.WriteLine("\t3. Start Listening Activity");
            Console.WriteLine("\t4. Quit");
            Console.Write("Select a choice from the menu: ");
            input = Convert.ToInt32(Console.ReadLine());
            if (input == 1)
            {
                BreathingActivity breathing = new BreathingActivity("Breathing Activity.",
                "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing");
                breathing.InteractBreathing();
            }

            if (input == 2)
            {
                ReflectionActivity reflection = new ReflectionActivity("Reflection Activity.",
                    "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");
                reflection.InteractReflection();
            }

            if (input == 3)
            {
                ListerActivity lister = new ListerActivity("Lister Activity.",
                "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
                lister.InteractLister();
            }
        }
    }
}