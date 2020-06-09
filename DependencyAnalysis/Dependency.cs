///////////////////////////////////////////////////////////////////////////
//Dependency.cs - This package finds for the dependencies in each file    /
//                 based on the typename, filename, namespace name.       /
// ver 2.3                                                                /
// Language:    C#, 2008, .Net Framework 4.0                              /
// Platform:    Dell Precision T7400, Win7, SP1                           /
// Application: Demonstration for CSE681, Project #2, Fall 2011           /                                        
//Author:       Amruta Joshi                                              / 
// CSE681 :     Software Modeling and Analysis, Fall 2018                 /
///////////////////////////////////////////////////////////////////////////
/*
 /*Package Operations:
 * ------------------
 * This package is used for the dependency analysis between the files based 
 * on thee typename, namespace name and filename. It provides the rules for 
 * various cases where same namespace is used in different files, but still
 * there is a dependency.
 */
/* Required Files:
*   Typetab.cs,
*   Semi.cs
*/
/*Maintenance History
 * -------------------
 * ver 1.0 : 31 Oct 2018
 * - first release
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysis
{
    using Lexer;
    public class Dependency
    {
        public static Dictionary<string, List<string>> typeTable1 = Typetab.getUsingTable();
        public static Dictionary<string, List<Typetab.TypeItem>> typeTableItems = Typetab.getTable();
        public static Dictionary<string, List<string>> storedFiles = new Dictionary<string, List<string>>();

        public static void checkDependency(List<string> files)
        {
            foreach (var file in files)
            {
                List<Typetab.TypeItem> tempList = new List<Typetab.TypeItem>();
                ITokenCollection semi = Factory.create();
                //semi.displayNewLines = false;
                if (!semi.open(file as string))    //------<Opens the file>---------------
                {
                    Console.Write("\n  Can't open {0}\n\n", file);
                    return;
                }
                else
                {
                    while (semi.get().Count > 0)
                    {
                        foreach (var sems in semi)
                        {
                            if (typeTableItems.ContainsKey(sems))
                            {
                                checkTable(sems, tempList, file);
                            }
                            else
                            {
                                checkUsingTable(tempList, (Path.GetFileName(file)));
                            }
                        }
                    }
                }
            }
        }

        public static void checkTable(string sems, List<Typetab.TypeItem> tempList, string file)
        {
            foreach (var typeitem in typeTableItems[sems])
            {
                if (Path.GetFileName(file) != typeitem.file && typeitem.nameSpace != "")

                    tempList.Add(typeitem);
            }
            if (tempList.Count == 1)
            {
                foreach (var templi in tempList)
                {
                    if (storedFiles.ContainsKey(templi.file))
                        storedFiles[templi.file].Add(Path.GetFileName(file));
                    else
                    {
                        List<string> fileList = new List<string>();
                        fileList.Add(Path.GetFileName(file));
                        storedFiles.Add(templi.file, fileList);
                    }
                }
            }

        }
        //------------> Dependency Checking for using >------------------------------
        private static void checkUsingTable(List<Typetab.TypeItem> tempList, string filename)
        {
            foreach (var temp in tempList)
            {
                if (typeTable1.ContainsKey(temp.nameSpace))
                {
                    foreach (var tem in typeTable1[temp.nameSpace])
                    {
                        if (filename != tem)
                        {
                            if (storedFiles.ContainsKey(temp.file))
                                storedFiles[temp.file].Add(filename);
                            else
                            {
                                List<string> fileList = new List<string>();
                                fileList.Add(filename);
                                storedFiles.Add(temp.file, fileList);
                            }
                        }

                    }
                }
            }
        }
        //----------->Returns the stored dependent files>-----------------------
        public static Dictionary<string, List<string>> getDependencyTable()
        {
            return storedFiles;
        }

#if (TEST_DEPENDENCYANALYSE)

        static void Main(string[] args)
        {
            Console.Write("\n DEPENDENCY ANALYSIS DEMONSTRATION ");
            Console.Write("\n ************************************\n");
            List<string> ListOfFiles = new List<string>();
            string FirstFile = "../../../Parser/Test.txt";
            string SecondFile = "../../../Parser/Test2.txt";
            string ThirdFile = "../../../Parser/Test3.txt";
            string path1 = System.IO.Path.GetFullPath(FirstFile);
            ListOfFiles.Add(path1);
            string path2 = System.IO.Path.GetFullPath(SecondFile);
            ListOfFiles.Add(path2);
            string path3 = System.IO.Path.GetFullPath(ThirdFile);
            ListOfFiles.Add(path3);
            checkDependency(ListOfFiles);
            Console.Write("\n\n");
        }
#endif

    }
}
