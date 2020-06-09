///////////////////////////////////////////////////////////////////////////
// strongcomponents.cs -  This package implements the Tarjan Algorithm   //
//                        for finding the strong Components in the graph.//
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
 * This package implements the Tarjan Algorithm to find the
 * strong components in the graph so that all files can be 
 * can be reached from any other file in the set by following
 * direct or transitive dependency links. 
 */
/* Required Files:
 *   graph.cs
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
    public class strongcomponents
    {
        public List<CsNode<string, string>> nodes { get; } = new List<CsNode<string, string>>();
        public Dictionary<CsNode<string, string>, List<CsNode<string, string>>> listOfGraphs { get; } = graph.getGraph();
        // Tarjan's Algorithm for the strongly connected components.  
        public void TarjanAlgo()
        {
            var index = 0;
            var Stack = new Stack<CsNode<string, string>>();
            Action<CsNode<string, string>> strongComp = null;
            strongComp = (node) =>
            {
                node.Index = index;
                node.LowLink = index;
                index++;
                Stack.Push(node);
                foreach (var child in listOfGraphs[node])
                {
                    if (child.Index < 0)
                    {
                        strongComp(child);
                        node.LowLink = Math.Min(node.LowLink, child.LowLink);
                    }
                    // Since the Successor w is in stack S thus in the current SCC
                    else if (Stack.Contains(child))
                        node.LowLink = Math.Min(node.LowLink, child.Index);
                }

                // If the root node is v, then pop the stack and generate the strong Connect Component
                if (node.LowLink == node.Index)
                {
                    //Console.Write("/=======================================/");
                    Console.Write("\n\n Strong Connect Component: ");

                    CsNode<string, string> w;
                    do
                    {
                        w = Stack.Pop();
                        Console.Write(w.name + " ");
                    } while (w != node);

                    Console.WriteLine();
                }
            };

            foreach (var graph in graph.getGraph())
            {
                nodes.Add(graph.Key);
            }
            foreach (var node in nodes)
                if (node.Index < 0)
                    strongComp(node);
        }
    }
}
