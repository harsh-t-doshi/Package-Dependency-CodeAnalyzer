///////////////////////////////////////////////////////////////////////
// Executive.cs - Demonstrate Prototype Code Analyzer                //
// ver 1.0                                                           //
// Language:    C#, 2017, .Net Framework 4.7.1                       //
// Platform:    Dell Precision T8900, Win10                          //
// Application: Demonstration for CSE681, Project #3, Fall 2018      //
// Source:      Jim Fawcett                                          //
// Author:      Amruta Joshi                                         // 
///////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * This package defines the following class:
 *   Executive:
 *   - uses Parser, RulesAndActions, Semi, and Toker to perform basic
 *     code metric analyzes
 */
/* Required Files:
 *   Executive.cs
 *   Parser.cs
 *   IRulesAndActions.cs, RulesAndActions.cs, ScopeStack.cs, Elements.cs
 *   ITokenCollection.cs, Semi.cs, Toker.cs
 *   Display.cs
 *   
 * Maintenance History:
 * --------------------
 * ver 1.1 : 06 Nov 2018
 * - Called the display methods to display the specific output as per requirements.
 * ver 1.0 : 09 Oct 2018
 * - first release
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CodeAnalysis
{
    using Lexer;

    class Executive
    {
        //----< process commandline to get file references >-----------------

        static List<string> ProcessCommandline(string[] args)
        {
            List<string> files = new List<string>();
            if (args.Length < 1)
            {
                Console.Write("\n  Please enter path and file(s) to analyze\n\n");
                return files;
            }
            string path = args[0];
            if (!Directory.Exists(path))
            {
                Console.Write("\n  invalid path \"{0}\"", System.IO.Path.GetFullPath(path));
                return files;
            }
            path = Path.GetFullPath(path);
            files.AddRange(Directory.GetFiles(path));
            return files;
        }

        static void ShowCommandLine(string[] args)
        {
            Console.Write("\n  Commandline args are:\n  ");

            Console.Write("  {0}", args[0]);

            Console.Write("\n  current directory: {0}", Path.GetFullPath(args[0]));
            Console.Write("\n");
        }

        static void Main(string[] args)
        {


            //Console.Write("\n  Demonstrating Parser");
            //Console.Write("\n ======================\n");
            // Console.Write("\n\n Type Table");
            ShowCommandLine(args);

            List<string> files = ProcessCommandline(args);
            Console.Write("\n\n");

            Console.Write("***************PROJECT 3 REQUIREMENTS******************************");
            Console.Write("\n\n");

            Requirement1();
            Requirement2();
            Requirement3();
            Requirement4(files);
            Requirement5(files);
            Requirement6();
            Requirement7();
            Requirement8();
            Console.Write("\n\n");
            Console.ReadLine();
        }


        private static void Requirement1()
        {
            StringBuilder msg = new StringBuilder();
            msg.Append("*************************REQUIREMENT 1***************************" + "\n");
            msg.Append("Implemented Project 3 using Visual Studio 2017 and C# windows Console Project" + "\n\n");
            Console.WriteLine(msg);
        }

        private static void Requirement2()
        {
            StringBuilder msg = new StringBuilder();
            msg.Append("*********************** REQUIREMENT 2 ********************************" + "\n");
            msg.Append("Implemented Project 3 using .NetSystem.IO  and System.Text for all I/O" + "\n\n");
            Console.WriteLine(msg);
        }
        private static void Requirement3()
        {
            StringBuilder msg = new StringBuilder();
            msg.Append("************************REQUIREMENT 3 ************************************" + "\n");
            msg.Append("The Project 3 contains Packages Namely :" + "\n");
            msg.Append("1) Tokenizer" + "\n");
            msg.Append("2) SemiExpression" + "\n");
            msg.Append("3) TypeTable" + "\n");
            msg.Append("4) TypeTable Analysis" + "\n");
            msg.Append("5) Dependency Analysis" + "\n");
            msg.Append("6) Strong Component" + "\n");
            msg.Append("7) Display" + "\n");
            msg.Append("8) Tester" + "\n");
            Console.Write(msg);
            Console.Write("\n\n");

        }
        private static void Requirement4(List<string> files)
        {
            StringBuilder msg = new StringBuilder();

            msg.Append("******************REQUIREMENT 4 ****************************" + "\n");
            msg.Append("TypeTable and UsingTable are generated based on the typeAnalysis and the rules defined");

            Console.Write(msg);
            try
            {
                typeAnalys.performTypeAnalysis(files);
                Display.showTypeTable();
                Display.showUsingTable();
                Console.Write("\n\n");
            }
            catch (Exception e)
            {
                Console.Write("An Exception an occured: '{0}'", e);
            }
        }
        private static void Requirement5(List<string> files)
        {
            StringBuilder msg = new StringBuilder();
            msg.Append("********************REQUIREMENT 5 *****************************" + "\n");
            msg.Append("Dependency Analysis is generated based on the typeTable");

            Console.Write(msg);
            try
            {
                Dependency.checkDependency(files);
                Display.showDependency();
            }
            catch (Exception e)
            {
                Console.Write("An Exception an occured: '{0}'", e);
            }
            Console.Write("\n\n");
        }
        private static void Requirement6()
        {
            StringBuilder msg = new StringBuilder();
            msg.Append("***********************REQUIREMENT 6 ***************************" + "\n");
            msg.Append("Strong Component for all the files is generated");

            Console.Write(msg);
            try
            {
                graph.Graphs();
                strongcomponents components = new strongcomponents();
                components.TarjanAlgo();
            }
            catch (Exception e)
            {
                Console.Write("An Exception an occured: '{0}'", e);
            }
            Console.Write("\n\n\n");
        }
        private static void Requirement7()
        {
            StringBuilder msg = new StringBuilder();
            msg.Append("******************************REQUIREMENT 7*********************************" + "\n");
            msg.Append("Result is displayed with the well formated area of the output" + "\n\n\n");
            Console.Write(msg);
        }
        private static void Requirement8()
        {
            StringBuilder msg = new StringBuilder();
            msg.Append("******************************REQUIREMENT 8***********************************" + "\n");
            msg.Append("Automated Test Unit is implemented that illustrates that all the requirements are implemented" + "\n");
            Console.Write(msg);
        }
    }
}
