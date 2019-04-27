using System;

namespace ConsoleBasedMenuPoC
{
    public static class MenuAction
    {
        public static void Quit()
        {
            Environment.Exit(0);
        }

        public static void ActionA()
        {
            Console.WriteLine("Action A");
        }

        public static void ActionB()
        {
            Console.WriteLine("Action B");
        }

        public static void ActionC()
        {
            Console.WriteLine("Action C");
        }
    }
}
