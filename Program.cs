using System;
using System.IO;

namespace FizzleCompiler
{
    public class Program
    {

        private static void Main(string[] args)
        {
            // Create a director called output used to create dump files into through the Data class
            Directory.CreateDirectory("Output");

            Functions.AddLibraries("using System.Collections.Generic;", "using System.Linq;", "using System;");
            Functions.AddNamespace("\nnamespace Fizzle");
            Functions.AddBracket('{');
            Functions.AddMainFunction("int age = GetAge()", "string name = GetName()", "Console.WriteLine($\"Name: {name}, Age: {age}\")", "int newVal = DoubleToInt(4.5)", "Console.WriteLine(newVal)");

            Functions.CreateFunction("private", true, "int", "GetAge", null, null, "age", "int age = 18");
            Functions.CreateFunction("private", true, "string", "GetName", null, null, "name", "string name = \"Fizzle\"");
            Functions.CreateFunction("internal", true, "int", "DoubleToInt", new[] { "double" }, new[] { "val" }, "Convert.ToInt32(val);");
            // End class bracket
            Functions.AddBracket('}');
            // End namespace bracket
            Functions.AddBracket('}');
            File.WriteAllLines(Data.OutputFile, Data.lines);


        }
    }
}