/////////////////////////////////////////////////////////////////////////////////////
// ITokenCollection.cs: This package acts as an interface for SemiExpression        /
//                      package.                                                    /                                                                           /            
// ver 2.3                                                                          /
// Language:    C#, 2008, .Net Framework 4.0                                        /
// Platform:    Dell Precision T7400, Win7, SP1                                     /
// Application: Demonstration for CSE681, Project #2, Fall 2011                     /
// Source:      Jim Fawcett                             / 
// CSE681 :     Software Modeling and Analysis, Fall 2018                           /  
/////////////////////////////////////////////////////////////////////////////////////

/*Package Operations:
 * -------------------
 * This package acts as an interface for SemiExpression package. It provides
 * different methods to check for the terminating conditions.
 * 
 * Public Interface:
 * -----------------
 *   bool open(string fullpath)
 *   bool open(string source);                
 *   void close();                                                          
 *   int size();                               
 *   Token this[int i] { get; set; }           
 *  ITokenCollection add(Token token);        
 *   bool insert(int n, Token tok);            
 *   void clear();                             
 *   bool contains(Token token);               
 *   bool find(Token tok, out int index);      
 *   Token predecessor(Token tok);             
 *   bool hasSequence(params Token[] tokSeq);  
 *   bool hasTerminator();                     
 *  bool isDone();                          
 *   int lineCount();                          
 *   string ToString();
 * string filename();
 * 
 *
 *Maintenance History
 * ----------------------------------------------------------------------- 
 *ver 1.0 : 07 October 2018
 * - first release

 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer
{
  using Token = String;
  using TokColl = List<String>;

  public interface ITokenCollection : IEnumerable<Token>
  {
    bool open(string source);                 // attach toker to source
    void close();                             // close toker's source
    TokColl get();                            // collect semi
    int size();                               // number of tokens
    Token this[int i] { get; set; }           // index semi
    ITokenCollection add(Token token);        // add a token to collection
    bool insert(int n, Token tok);            // insert tok at index n
    void clear();                             // clear all tokens
    bool contains(Token token);               // has token?
    bool find(Token tok, out int index);      // find tok if in semi
    Token predecessor(Token tok);             // find token before tok
    bool hasSequence(params Token[] tokSeq);  // does semi have this sequence of tokens?
    bool hasTerminator();                     // does semi have a valid terminator
    bool isDone();                            // at end of tokenSource?
    int lineCount();                          // get number of lines processed
    string ToString();                        // concatenate tokens with intervening spaces
    void show();                              // display semi on console
   string filename();
    }
}
