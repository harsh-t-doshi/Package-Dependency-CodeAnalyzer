///////////////////////////////////////////////////////////////////////////
//Typetab.cs - It provides the information about the typename, namespaces /
//             and files which is stored and based on this table the      /
//             rules are used to find the further dependency.             /
// ver 2.3                                                                /
// Language:    C#, 2008, .Net Framework 4.0                              /
// Platform:    Dell Precision T7400, Win7, SP1                           /
// Application: Demonstration for CSE681, Project #2, Fall 2011           /
//Source:       Jim Fawcett
//Author:       Amruta Joshi                                              / 
// CSE681 :     Software Modeling and Analysis, Fall 2018                 /
///////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * ------------------
 * This module stores the types of the different files like the class name
 * file name, namespace name which is used for the further dependency.
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysis
{
    //using File = String;
    //using Type = String;
    //using Namespace = String;

    public class Typetab
    {
        public static Dictionary<string, List<string>> typeTable1 = new Dictionary<string, List<string>>();
        public static Dictionary<string, List<TypeItem>> typeTableItems = new Dictionary<string, List<TypeItem>>();

        public void add(string typeName, string nameSpace, string filename)
        {
            TypeItem typeItem = new TypeItem();
            typeItem.file = filename;
            typeItem.nameSpace = nameSpace;

            if (typeTableItems.ContainsKey(typeName))
            {
                typeTableItems[typeName].Add(typeItem);
            }
            else
            {
                List<TypeItem> typeItemList = new List<TypeItem>();
                typeItemList.Add(typeItem);
                typeTableItems.Add(typeName, typeItemList);
            }
        }
        //-----------< Typetable for Using Table >----------------
        public void addTable(string typeName, string filename)
        {

            if (typeTable1.ContainsKey(typeName))
            {
                typeTable1[typeName].Add(filename);
            }
            else
            {
                List<string> usingList = new List<string>();
                usingList.Add(filename);
                typeTable1.Add(typeName, usingList);
            }
        }
        //-----< TypeItem class holds the File and Namespace >------------------------

        public class TypeItem
        {
            public string file { get; set; }
            public string nameSpace { get; set; }
        }
        //----< return the items stored in the typeTable >--------------
        public static Dictionary<string, List<TypeItem>> getTable()
        {
            return typeTableItems;
        }
        //------< return the using Table >-----------------------------
        public static Dictionary<string, List<string>> getUsingTable()
        {
            return typeTable1;
        }
#if (TEST_TYPETABLE)

        static void Main(string[] args)
        {
            Console.Write("\n TYPE TABLE DEMONSTRATION ");
            Console.Write("\n ************************************\n");
            // List<string> ListOfFiles = new List<string>();
            string FirstFile = "../../../Parser/Test.txt";
            //string SecondFile = "../../../Parser/Test2.txt";
            string path1 = System.IO.Path.GetFullPath(FirstFile);
            string nameSpace = "SemiExpression";
            string typename = "semi";
            Typetab typetable = new Typetab();
            typetable.add(typename, nameSpace, FirstFile);
            typetable.addTable(typename, path1);
            Console.Write("\n\n");
        }
#endif

    }

}









   

