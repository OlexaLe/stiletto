using System;
using System.Collections.Generic;

namespace Example.Console
{
    internal class Program
    {
        public static void Main(string[] args)
        {
//            CoffeeApp.Main();
            var a = new CoffeeApp();
            a.Run();
            System.Console.ReadKey();
        }
    }
}