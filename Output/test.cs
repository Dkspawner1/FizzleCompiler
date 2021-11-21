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
        internal static int DoubleToInt(double val)
        {
            return Convert.ToInt32(val);
        }
        private static string GetName() => Console.ReadLine();
        private static string GetNameParams(string test) => test;
    }
}
