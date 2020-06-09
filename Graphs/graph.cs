///////////////////////////////////////////////////////////////////////////
// graph.cs:              This package is required while implementing    //
//                        the tarjan's Algorithm which gives the details //
//                        about the nodes.                               //
// ver 1.0                                                               //
// Language:    C#, 2017, .Net Framework 4.7.1                           //
// Platform:    Dell Precision T8900, Win10                              //
// Application: Demonstration for CSE681, Project #3, Fall 2018          //
// Source:      Dr.Jim Fawcett                                           //
// Author Name: Amruta Joshi                                             //          
// CSE681 :     Software Modeling and Analysis, Fall 2018                //                                                        //
///////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * This package provides the information about the nodes,edges
 * which is used for implementing Tarjan's Algorithm.
 */
/* Required Files:
 *   Dependency.cs
 *   
 * Maintenance History:
 * --------------------
 * ver 1.0 : 06 November 2018 
 * * - first release
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalysis
{
    //----< holds child node and instance of edge type E>------------

    public class CsEdge<V, E>
    {
        public CsNode<V, E> targetNode { get; set; } = null;
        public E edgeValue { get; set; }

        public CsEdge(CsNode<V, E> node, E value)
        {
            targetNode = node;
            edgeValue = value;
        }
    };

    public class CsNode<V, E>
    {
        public V nodeValue { get; set; }
        public string name { get; set; }
        public List<CsEdge<V, E>> children { get; set; }
        public bool visited { get; set; }
        public int Index { get; set; }
        public int LowLink { get; set; }

        //----< construct a named node >---------------------------------------

        public CsNode(string nodeName)
        {
            name = nodeName;
            children = new List<CsEdge<V, E>>();
            visited = false;
            Index = -1;
            LowLink = 0;
        }
        //----< add child vertex and its associated edge value to vertex >-----

        public void addChild(CsNode<V, E> childNode, E edgeVal)
        {
            children.Add(new CsEdge<V, E>(childNode, edgeVal));
        }

    }

    public class graph
    {
        public static Dictionary<string, List<string>> dependentFiles = Dependency.getDependencyTable();
        public static Dictionary<string, CsNode<string, string>> ListOfNodes = new Dictionary<string, CsNode<string, string>>();
        public static Dictionary<CsNode<string, string>, List<CsNode<string, string>>> allgraphs = new Dictionary<CsNode<string, string>, List<CsNode<string, string>>>();

        static public void Graphs()
        {
            foreach (var depend in dependentFiles)
            {
                CsNode<string, string> node;
                if (ListOfNodes.ContainsKey(depend.Key))
                {
                    node = ListOfNodes[depend.Key];
                }
                else
                {
                    node = new CsNode<string, string>(depend.Key);
                    ListOfNodes.Add(depend.Key, node);
                }
                foreach (var child in depend.Value)
                {
                    CsNode<string, string> nodes;
                    if (ListOfNodes.ContainsKey(child))
                    {
                        nodes = ListOfNodes[child];
                    }
                    else
                    {
                        nodes = new CsNode<string, string>(child);
                        ListOfNodes.Add(child, nodes);
                    }
                    node.addChild(nodes, "edge");
                }
            }

            foreach (var node in ListOfNodes)
            {
                List<CsNode<string, string>> ListOfChild = new List<CsNode<string, string>>();
                CsNode<string, string> ParentNode = node.Value;
                foreach (var chidNode in node.Value.children)
                {
                    ListOfChild.Add(chidNode.targetNode);
                }
                allgraphs.Add(ParentNode, ListOfChild);    //-----<holds the ParentNode and List of Child Nodes------>
            }
        }
        static public Dictionary<CsNode<string, string>, List<CsNode<string, string>>> getGraph()
        {
            return allgraphs;
        }


#if (TEST_GRAPHS)

        static void Main(string[] args)
        {
            Console.Write("\n GRAPH DEMONSTRATION ");
            Console.Write("\n ************************************\n");
            List<string> ListOfFiles = new List<string>();
            string FirstFile = "../../../Parser/Test.txt";
            string path1 = System.IO.Path.GetFullPath(FirstFile);
            ListOfFiles.Add(path1);
            string SecondFile = "../../../Parser/Test2.txt";
            string path2 = System.IO.Path.GetFullPath(SecondFile);
            ListOfFiles.Add(path2);
            string ThirdFile = "../../..Parser/Test3.txt";
            string path3 = System.IO.Path.GetFullPath(ThirdFile);
            ListOfFiles.Add(path3);

            dependentFiles.Add(path1, ListOfFiles);
            getGraph();
            Console.Write("\n\n");

        }
#endif
    }
}







       





