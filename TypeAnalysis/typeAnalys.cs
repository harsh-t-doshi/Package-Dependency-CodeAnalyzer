///////////////////////////////////////////////////////////////////////////
//TypeAnalys.cs: It stores the types of the file along with the filename  /
//               and namespace name to do the further analysis required   /
//               for the typetable and dependency analysis.               /
// ver 2.3                                                                /
// Language:    C#, 2008, .Net Framework 4.0                              /
// Platform:    Dell Precision T7400, Win7, SP1                           /
// Application: Demonstration for CSE681, Project #2, Fall 2011           /
// Author:       Amruta Joshi                                              / 
// CSE681 :     Software Modeling and Analysis, Fall 2018                 /
///////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * ------------------
 * It stores the types of the file along with the filename  
 * and namespace name to do the further analysis required   
 * for the typetable and dependency analysis.  
 */

/* Required Files:
 *   Typetab.cs,
 *   Semi.cs
 *   Dependency.cs
 *   Parser.cs
 *   
 */
/*Maintenance History
 * -------------------
 * ver 1.0 : 02 Nov 2018
 * - first release
 */

using Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysis
{
    public static class typeAnalys
    {
        public static void performTypeAnalysis(List<string> files)
        {
            foreach (string file in files)
            {
                // Console.Write("\n  Processing file {0}\n", System.IO.Path.GetFileName(file));

                Lexer.ITokenCollection semi = Factory.create();
                //semi.displayNewLines = false;
                if (!semi.open(file as string))
                {
                    Console.Write("\n  Can't open {0}\n\n", files);
                    return;
                }

                BuildCodeAnalyzer builder = new BuildCodeAnalyzer(semi);
                Parser parser = builder.build();

                try
                {
                    while (semi.get().Count > 0)
                        parser.parse(semi);
                }
                catch (Exception ex)
                {
                    Console.Write("\n\n  {0}\n", ex.Message);
                }
                Repository rep = Repository.getInstance();
                List<Elem> table = rep.locations;
                
                Console.Write("\n");
                rep.nameSpace = "";
                semi.close();
            }
        }
#if (TEST_TYPEANALYSE)

        static void Main(string[] args)
        {
            Console.Write("\n TYPE ANALYSIS DEMONSTRATION ");
            Console.Write("\n ************************************\n");
            List<string> ListOfFiles = new List<string>();
            string FirstFile = "../../../Parser/Test.txt";
            string SecondFile = "../../../Parser/Test2.txt";
            string path1 = System.IO.Path.GetFullPath(FirstFile);
            ListOfFiles.Add(path1);
            string path2 = System.IO.Path.GetFullPath(SecondFile);
            ListOfFiles.Add(path2);
            performTypeAnalysis(ListOfFiles);
            Console.Write("\n\n");
        }
#endif
    }
}



    