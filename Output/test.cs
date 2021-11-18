using System;
using System.Linq;
using System.Collections.Generic;

namespace Fizzle
{
    public class Program
    {
        private static void MainDebug(string[] args)
        {
            int age = GetAge();
            string name = GetName();
            Console.WriteLine($"Name: {name}, Age: {age}");
            int newVal = DoubleToInt(4.5);
            Console.WriteLine(newVal);
        }
        private static int GetAge()
        {
            int age = 18;
            return age;
        }
        private static string GetName()
        {
            string name = "Fizzle";
            return name;
        }
        internal static int DoubleToInt(double val)
        {
            return Convert.ToInt32(val); ;
        }
    }
}
