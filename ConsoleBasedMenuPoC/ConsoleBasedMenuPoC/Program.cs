using System;

namespace ConsoleBasedMenuPoC
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleBasedMenuPoC.ui.Console.FullScreen();
            Menu.Initialize();
            Console.ReadLine();
        }
    }
}
