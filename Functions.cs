using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace FizzleCompiler
{
    // This class contains all the functions to start creating a c# file
    public static class Functions
    {
        // This function will add the namespace after the libraries, if the file exists this function
        // Will search for when the namespaces end and insert there or if creating a new file will insert right after the libraries
        public static void AddNamespace(string name)
        {
            if (File.Exists(Data.OutputFile))
            {
                int indexAfterLibraries = 0;
                var contents = File.ReadAllLines(Data.OutputFile);

                for (int i = 0; i < contents.Length; i++)
                {
                    string line = contents[i];
                    if (line.Split(' ')[0].Equals("using"))
                        continue;
                    else
                    {
                        indexAfterLibraries = i;
                        break;
                    }
                }
                Data.lines.Insert(indexAfterLibraries, name);
            }
            else Data.lines.Add(name);
        }
        // This function adds libraries to the top of the program which has to start with "using" and ends with ; just like writing the normal file.  
        public static void AddLibraries(params string[] libraries)
        {
            const int START_INDEX = 0;

            foreach (var library in libraries)
            {
                // Split string at char SPACE which index 0 should return using and index 1 is the library 
                // var t = library.Split(' ')[1];
                // System.Console.WriteLine(t);

                // Make sure the user has entered correct syntax starting with using followed by a space
                if (library.Split(' ')[0].Equals("using") is false)
                    throw new Exception("Improper Library Prefix");
                // Check if the end of the line ends with a semi-colon if false returns a exception
                else if (library.EndsWith(';') is false)
                    throw new Exception("Libraries must end with a semi-colon");
                // If string passes all checks insert the string into the program at index 0 pushing the other lines down 1 index 
                else
                    Data.lines.Insert(START_INDEX, library);
            }
        }

        /*
        Adds the entry point which the compiler reads first, has to be called Main
        The format is as follows:
        public class program
        {
            private static void Main(string[] args)
            {
                Console.WriteLine("Hello, World!");
            }
        }
        */
#nullable enable
        public static void AddMainFunction(params string[] body)
        {
            Data.lines.Add("public class Program");
            Data.lines.Add("{");

#if DEBUG
            // Wrapper for debug so that program doesn't get confused with two main function
            Data.lines.Add("private static void MainDebug(string[] args)");
#else
            // If program is in release then use actual main function
            Data.lines.Add("private static void Main(string[] args)");
#endif
            AddBracket('{');
            foreach (var line in body)
                Data.lines.Add($"{line}{(!line.EndsWith(';') ? ";" : string.Empty)}");
            AddBracket('}');
        }
#nullable disable
        // Allow for partial classes to be complete for example if user wants no parameters 
#nullable enable
        public static void CreateLambdaFunction(string accessibilityModifier, bool isStatic, string type, string funcName, string[] variableTypes, string[] variableNames, string returnVariable)
        {
            if (!(accessibilityModifier is "private" or "public" or "internal"))
                throw new Exception("Invalid Modifier");
            Data.lines.Add($"{accessibilityModifier} {(isStatic ? "static" : String.Empty)} {type} {funcName}(");


            if (variableNames is not null)
            {

                for (int i = 0; i < variableTypes.Length; i++)
                {
                    string? vType = variableTypes[i];
                    string? vName = variableNames[i];
                    Data.lines[Data.lines.Count - 1] += $"{vType} {vName}";
                }
                // add lambda function: 
                Data.lines[Data.lines.Count - 1] += $") => {returnVariable};";
            }
            else
                Data.lines[Data.lines.Count - 1] += $") => {returnVariable};";

            // Data.lines[Data.lines.Count - 1] += ";";
        }

        public static void CreateFunction(string accessibilityModifier, bool isStatic, string type, string funcName, string[] variableTypes, string[] variableNames, string returnVariable, params string[] Body)
        {
            if (!(accessibilityModifier is "private" or "public" or "internal"))
                throw new Exception("Invalid Modifier");

            // This line creates an interpolated string with a ternary expression for adding static or not
            Data.lines.Add($"{accessibilityModifier} {(isStatic ? "static" : String.Empty)} {type} {funcName}(");

            if (variableNames is not null)
            {
                if (variableNames.Length != variableTypes.Length)
                    throw new Exception("One or more variable(s) does not have a name or type");

                for (int i = 0; i < variableNames.Length; i++)
                {
                    Data.lines[Data.lines.Count - 1] += $"{variableTypes[i]} {variableNames[i]}";
                    if (i != variableNames.Length - 1)
                        Data.lines[Data.lines.Count - 1] += ", ";
                }
            }
            // Appends the previous line to add )
            Data.lines[Data.lines.Count - 1] += ")";


            AddBracket('{');
            foreach (var line in Body)
                Data.lines.Add($"{line};");

            // Adds return functionality 

            if (returnVariable is not null && type is not "void")
                Data.lines.Add($"return {returnVariable};");
            AddBracket('}');
        }
        // re-enable checks for null values
#nullable disable
        // Used to add the odd curly bracket at the end or start of the program 
        public static void AddBracket(char bracket)
        {
            if (bracket is '}' or '{')
                Data.lines.Add(bracket.ToString());
            else throw new Exception("Invalid Bracket Char");
        }
        public static void CreateVariable(Type type, string name, object value)
        {

        }
        // User MUST return a correct value for this to work, otherwise it will throw a exception 
        public static void CreateGlobalVariable()
        {

        }
        public static void CreateLocalVariable()
        {


        }
    }
}