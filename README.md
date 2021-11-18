"# FizzleCompiler" 

This following code block is an example from the source file and is what appears in the finished output file 

```c#
Functions.AddLibraries("using System.Collections.Generic;", "using System.Linq;", "using System;");
Functions.AddNamespace("\nnamespace Fizzle");
Functions.AddBracket('{');
Functions.AddMainFunction("int age = GetAge()", "string name = GetName()", "Console.WriteLine($\"Name: {name}, Age: {age}\")", "int newVal = DoubleToInt(4.5)", "Console.WriteLine(newVal)");

Functions.CreateFunction("private", true, "int", "GetAge", null, null, "age", "int age = 18");
Functions.CreateFunction("private", true, "string", "GetName", null, null, "name", "string name = \"Fizzle\"");
Functions.CreateFunction("internal", true, "int", "DoubleToInt", new[] { "double" }, new[] { "val" }, "Convert.ToInt32(val);");

Functions.AddBracket('}');
Functions.AddBracket('}');
```
The Finished output file:
```
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

```
